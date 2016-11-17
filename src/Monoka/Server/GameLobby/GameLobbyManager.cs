using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Server.GameSession;

namespace Monoka.Server.GameLobby
{
    public class GameLobbyManager : LoggingReceiveActor, IMessageSubscriber
    {
        private readonly IIoC _ioc;
        private readonly List<GameLobby> _gameLobbies;
        private readonly ActorSelection _clientRegistry;
        private readonly ActorSelection _gameSessionManager;

        public GameLobbyManager(IIoC ioc, ILogger log) : base(log)
        {
            if (ioc == null) throw new ArgumentNullException(nameof(ioc));

            _ioc = ioc;
            _gameLobbies = new List<GameLobby>();
            
            _clientRegistry = Context.System.ActorSelection(ActorRegistry.ClientRegistry, ActorPathType.Relative);
            _gameSessionManager = Context.System.ActorSelection(ActorRegistry.GameSessionManager, ActorPathType.Relative);

            Receive<GetGameLobbies>(msg => Sender.Tell(_gameLobbies, Self));
            Receive<CreateNewGameLobby>(msg => OnCreateNewGameLobby(msg));
            Receive<PlayerReady>(msg => OnPlayerReady(msg));
            Receive<GetGameLobby>(msg => OnGetGameLobby(msg));
            ReceiveAsync<JoinGameLobby>(async msg => await OnJoinGameLobby(msg));
            Receive<IsAnyLobbyLookingForPlayers>(msg => OnIsAnyLobbyLookingForPlayers(msg));
        }

        private void OnIsAnyLobbyLookingForPlayers(IsAnyLobbyLookingForPlayers message)
        {
            try
            {
                var gameLobby = _gameLobbies.FirstOrDefault(l => l.Players.Count < GameLobby.RequiredNrOfPlayers);

                Sender.Tell(gameLobby, Self);
            }
            catch (Exception ex)
            {
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private void OnGetGameLobby(GetGameLobby msg)
        {
            try
            {
                var lobby = _gameLobbies.Single(g => g.Id == msg.GameLobbyId);

                Sender.Tell(new GetGameLobbyResult(lobby), Self);
            }
            catch (Exception ex)
            {
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private void OnPlayerReady(PlayerReady msg)
        {
            try
            {
                var lobby = _gameLobbies.SingleOrDefault(s => s.Id == msg.LobbyId);

                if (lobby != null)
                {
                    var player = lobby.Players.SingleOrDefault(p => p.Id == msg.PlayerId);

                    if (player != null)
                    {
                        player.IsReady = true;
                        
                        Sender.Tell(lobby, Self);

                        if (lobby.AreAllPlayersReady && IsEnoughPlayers(lobby))
                        {
                            _gameSessionManager.Tell(new GameSessionManager.NewSession(lobby));
                        }
                    }
                    else
                    {
                        Sender.Tell(new Failure
                        {
                            Exception =
                                new Exception($"Could not find player {msg.PlayerId}")
                        }, Self);
                    }
                }
                else
                {
                    Sender.Tell(new Failure
                    {
                        Exception = new Exception($"Could not find game lobby {msg.LobbyId}")
                    }, Self);
                }
            }
            catch (Exception ex)
            {
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private bool IsEnoughPlayers(GameLobby lobby)
        {
            return lobby.Players.Count == GameLobby.RequiredNrOfPlayers;
        }

        private async Task OnJoinGameLobby(JoinGameLobby msg)
        {
            try
            {
                var success = await TryJoinGameLobby(msg.LobbyId, msg.PlayerId, msg.AsAdmin);

                var lobby = _gameLobbies.Single(l => l.Id == msg.LobbyId);

                Sender.Tell(new JoinGameLobbyResult(msg.PlayerId, lobby, success), Self);
            }
            catch (Exception ex)
            {
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private void OnCreateNewGameLobby(CreateNewGameLobby msg)
        {
            try
            {
                var newLobby = CreateNewLobby(msg.GameLobbyName);
                
                Sender.Tell(new CreatedNewGameLobby(newLobby), Self);
            }
            catch (Exception ex)
            {
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }
        
        private async Task<bool> TryJoinGameLobby(Guid lobbyId, Guid playerId, bool asAdmin)
        {
            var lobby = _gameLobbies.SingleOrDefault(s => s.Id == lobbyId);

            if (lobby != null && !lobby.Players.Select(p => p.Id).Contains(playerId))
            {
                var answer = await _clientRegistry.Ask(new ClientRegistry.GetClient(playerId));

                if (answer is ClientDto)
                {
                    var client = answer as ClientDto;

                    var player = new GameLobbyPlayer
                    {
                        Id = playerId,
                        Name = client.Username,
                        IsReady = false,
                        IsAdmin = asAdmin
                    };

                    lobby.Join(player);

                    return true;
                }
            }

            return false;
        }

        private bool RequestLeaveSession(Guid sessionId, Guid playerId)
        {
            var gameLobby = _gameLobbies.SingleOrDefault(s => s.Id == sessionId);

            if (gameLobby != null && !gameLobby.Players.Select(p => p.Id).Contains(playerId))
            {
                gameLobby.Leave(playerId);
                return true;
            }
            return false;
        }

        private GameLobby CreateNewLobby(string lobbyTitle)
        {
            var newGameLobby = _ioc.Resolve<GameLobby>();

            newGameLobby.Id = Guid.NewGuid();
            newGameLobby.Title = lobbyTitle;

            _gameLobbies.Add(newGameLobby);

            return newGameLobby;
        }

        #region Messages

        public class GetGameLobbyResult
        {
            public GameLobby GameLobby { get; }

            public GetGameLobbyResult(GameLobby gameLobby)
            {
                GameLobby = gameLobby;
            }
        }

        public class CreatedNewGameLobby
        {
            public GameLobby GameLobby { get; }

            public CreatedNewGameLobby(GameLobby gameLobby)
            {
                GameLobby = gameLobby;
            }
        }

        public class JoinGameLobby
        {
            public JoinGameLobby(Guid playerId, Guid lobbyId, bool asAdmin = false)
            {
                PlayerId = playerId;
                LobbyId = lobbyId;
                AsAdmin = asAdmin;
            }

            public Guid PlayerId { get; }
            public Guid LobbyId { get; }
            public bool AsAdmin { get; }
        }

        public class JoinGameLobbyResult
        {
            public Guid PlayerId { get; }
            public GameLobby GameLobby { get; }
            public bool Success { get; }

            public JoinGameLobbyResult(Guid playerId, GameLobby gameLobby, bool success)
            {
                GameLobby = gameLobby;
                Success = success;
                PlayerId = playerId;
            }
        }

        public class PlayerWasRejectedJoiningGameLobby
        {
            public Guid PlayerId { get; }
            public Guid LobbyId { get; }

            public PlayerWasRejectedJoiningGameLobby(Guid playerId, Guid lobbyId)
            {
                LobbyId = lobbyId;
                PlayerId = playerId;
            }
        }

        public class GetGameLobbies { }

        public class CreateNewGameLobby
        {
            public string GameLobbyName { get; }
            public Guid PlayerId { get; }

            public CreateNewGameLobby(string gameLobbyName, Guid playerId)
            {
                GameLobbyName = gameLobbyName;
                PlayerId = playerId;
            }
        }

        public class PlayerReady
        {
            public Guid PlayerId { get; }
            public Guid LobbyId { get; }

            public PlayerReady(Guid playerId, Guid lobbyId)
            {
                PlayerId = playerId;
                LobbyId = lobbyId;
            }
        }

        public class GetGameLobby
        {
            public Guid GameLobbyId { get; }

            public GetGameLobby(Guid gameLobbyId)
            {
                GameLobbyId = gameLobbyId;
            }
        }

        public class IsAnyLobbyLookingForPlayers { }

        #endregion
    }
}