using System;
using System.Threading.Tasks;
using Monoka.Client.GameLobby;
using Monoka.Client.Login;
using Monoka.Client.Login.Results;
using Monoka.Client.Model;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging.Contracts;

namespace Monoka.Client
{
    public class Matchmaker : LoggingReceiveActor
    {
        private readonly GameLobby.GameLobby _gameLobby;
        private readonly IGameLobbyFacade _gameLobbyApiFacade;
        private readonly ILoginFacade _loginFacade;

        public Matchmaker(IGameLobbyFacade gameLobbyApiFacade, ILoginFacade loginFacade, ILogger logger) : base(logger)
        {
            if (gameLobbyApiFacade == null) throw new ArgumentNullException(nameof(gameLobbyApiFacade));
            if (loginFacade == null) throw new ArgumentNullException(nameof(loginFacade));

            _gameLobbyApiFacade = gameLobbyApiFacade;
            _loginFacade = loginFacade;

            _gameLobby = new GameLobby.GameLobby();

            Become(NotLoggedIn);
        }

        private void NotLoggedIn()
        {
            ReceiveAsync<FindOponent>(async msg => await OnFindOponent(msg));
        }

        private void ReadyToStartGame()
        {

        }

        private async Task<HandshakeResult> InitiateHandshake(string playerName)
        {
            var result = await _loginFacade.Handshake(playerName);

            var player = new Player(result.PlayerId, playerName)
            {
                IsLocalPlayer = true
            };

            _gameLobby.Players.Add(player);
            
            return result;
        }

        private async Task OnFindOponent(FindOponent message)
        {
            var playerName = message.PlayerName ?? $"Player_{Guid.NewGuid().ToString().Substring(0, 4)}";

            var handshake = await InitiateHandshake(playerName);

            if (handshake.Success)
            {
                var playerId = handshake.PlayerId;

                var lookingForPlayers = await _gameLobbyApiFacade.IsAnyLobbyLookingForPlayers();

                if (lookingForPlayers.Success)
                {
                    var lobbyId = lookingForPlayers.GameLobby.Id;

                    var joinLobby =
                        await _gameLobbyApiFacade.JoinGameLobby(playerId, lobbyId);

                    if (joinLobby.Success)
                    {
                        await _gameLobbyApiFacade.PlayerReady(playerId, lobbyId);

                        Become(ReadyToStartGame);
                    }
                    else
                    {
                        Log.Msg(this, l => l.Info($"Player {playerId} could not join lobby {lobbyId}"));
                    }
                }
                else
                {
                    var gameLobbyName = $"Game_Lobby_{Guid.NewGuid().ToString().Substring(0, 4)}";

                    var createLobbyResult = await _gameLobbyApiFacade.CreateAndJoinGameLobby(playerId, gameLobbyName);

                    await _gameLobbyApiFacade.PlayerReady(playerId, createLobbyResult.Id);

                    Become(ReadyToStartGame);
                }
            }
            else
            {
                Log.Msg(this, l => l.Warning($"Could not log in player {playerName}"));
            }
        }

        #region Messages

        internal class FindOponent
        {
            public FindOponent(string playerName)
            {
                PlayerName = playerName;
            }

            public string PlayerName { get; }
        }

        #endregion  
    }
}
