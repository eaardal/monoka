using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Adapters;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network;
using Monoka.Server.GameLobby;
using Monoka.Server.Model;

namespace Monoka.Server.GameSession
{
    public class GameSession : LoggingReceiveActor
    {
        private readonly IAutoMapperAdapter _mapper;
        private IActorRef _gameLoop;

        private const int MinPlayers = 2;
        private const int MaxPlayers = 2;
        private readonly List<GameSessionPlayer> _players;
        private Guid _sessionId;
        private string _title;
        private readonly ActorSelection _clientRegistry;
        private readonly ActorSelection _gameSessionApi;

        public GameSession(ILogger log, IAutoMapperAdapter mapper) : base(log)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;

            _players = new List<GameSessionPlayer>();

            _clientRegistry = Context.ActorSelection(ActorRegistry.ClientRegistry);
            _gameSessionApi = Context.ActorSelection(RemoteActorRegistry.Server.GameSessionApi);

            Become(AwaitingInitialization);
        }

        #region Become

        private void AwaitingInitialization()
        {
            ReceiveAsync<CreateAndStartSession>(async msg => await OnCreateNewSessionFromLobby(msg));
        }

        private void Initialized()
        {

        }

        private void Playing()
        {
            Log.Msg(this, l => l.Debug($"Game session {_sessionId} became Playing"));
        }

        private void Loading()
        {
            Receive<PlayerFinishedLoading>(msg => OnPlayerFinishedLoading(msg));
        }

        private void OnPlayerFinishedLoading(PlayerFinishedLoading msg)
        {
            Log.Msg(this, l => l.Debug($"Player {msg.PlayerId} finished loading"));

            foreach (var player in _players)
            {
                if (player.Id == msg.PlayerId)
                {
                    player.FinishedLoading = true;
                }
            }

            if (_players.All(p => p.FinishedLoading))
            {
                Log.Msg(this, l => l.Debug("All players finished loading"));

                StartGame();
            }
        }

        private void StartGame()
        {
            var players = _mapper.Map<IEnumerable<GameSessionPlayer>, IEnumerable<Player>>(_players);

            _gameLoop = Context.ActorFromIoC<GameEngine.GameEngine>(ActorRegistry.GameEngine.NameWithArgs(_sessionId));
            _gameLoop.Tell(new GameEngine.GameEngine.StartGame(players, _sessionId));

            Become(Playing);
        }

        #endregion

        private async Task OnCreateNewSessionFromLobby(CreateAndStartSession msg)
        {
            _sessionId = msg.GameLobby.Id;
            _title = msg.GameLobby.Title;

            msg.GameLobby.Players.ForEach(p =>
            {
                var player = _mapper.Map<GameLobbyPlayer, Player>(p);
                var joined = Join(player);
                if (!joined)
                {
                    Sender.Tell(new TooManyPlayers(player), Self);
                }
            });

            Become(Initialized);

            await StartSession();
        }

        private async Task StartSession()
        {
            var answer = await _clientRegistry.Ask(new ClientRegistry.GetClients(_players.Select(p => p.Id)));

            if (answer is IEnumerable<ClientDto>)
            {
                var clients = answer as IEnumerable<ClientDto>;

                _gameSessionApi.Tell(new StartLoadingScreen(clients, _sessionId), Self);

                Become(Loading);
            }
            else
            {
                Log.Msg(this, l => l.Warning($"Unexpected answer {answer.GetType()} to {typeof(ClientRegistry.GetClients)} message"));
            }
        }
        
        private bool Join(Player player)
        {
            var canJoin = _players.Count < MaxPlayers;

            if (canJoin)
            {
                var gameSessionPlayer = _mapper.Map<Player, GameSessionPlayer>(player);
                _players.Add(gameSessionPlayer);
            }

            return canJoin;
        }
        
        #region Messages

        internal class TooManyPlayers
        {
            public Player Player { get; }

            public TooManyPlayers(Player player)
            {
                Player = player;
            }
        }

        internal class CreateAndStartSession
        {
            public GameLobby.GameLobby GameLobby { get; }

            public CreateAndStartSession(GameLobby.GameLobby gameLobby)
            {
                GameLobby = gameLobby;
            }
        }

        internal class StartLoadingScreen
        {
            public IEnumerable<ClientDto> Clients { get; }
            public Guid GameSessionId { get; }

            public StartLoadingScreen(IEnumerable<ClientDto> clients, Guid gameSessionId)
            {
                Clients = clients;
                GameSessionId = gameSessionId;
            }
        }

        internal class PlayerFinishedLoading
        {
            public PlayerFinishedLoading(Guid playerId)
            {
                PlayerId = playerId;
            }

            public Guid PlayerId { get; }
        }
        
        #endregion
    }
}
