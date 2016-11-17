using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monoka.Common.Dto;

namespace Monoka.Common.Network.Messages
{
    public class FromServer
    {
        public class AvailableGameLobbies
        {
            public IEnumerable<GameLobbyDto> GameLobbies { get; private set; }

            public AvailableGameLobbies(IEnumerable<GameLobbyDto> gameLobbies)
            {
                if (gameLobbies == null) throw new ArgumentNullException("GameLobbies");
                GameLobbies = gameLobbies;
            }
        }

        public class GameLobbyCreated
        {
            public GameLobbyCreated(GameLobbyDto gameLobby)
            {
                GameLobby = gameLobby;
            }

            public GameLobbyDto GameLobby { get; private set; }
        }

        public class GameLobbyUpdated
        {
            public GameLobbyDto GameLobby { get; private set; }

            public GameLobbyUpdated(GameLobbyDto gameLobby)
            {
                GameLobby = gameLobby;
            }
        }

        public class HandshakeResult
        {
            public HandshakeResult(bool success, Guid playerId)
            {
                Success = success;
                PlayerId = playerId;
            }

            public bool Success { get; private set; }
            public Guid PlayerId { get; private set; }
        }

        public class LoginResultMessage
        {
            public LoginResultMessage(bool wasAccepted, DateTime timestamp)
            {
                WasAccepted = wasAccepted;
                Timestamp = timestamp;
            }

            public bool WasAccepted { get; private set; }
            public DateTime Timestamp { get; private set; }
        }

        public class PlayerJoinedGameLobby
        {
            public PlayerDto Player { get; set; }
            public Guid LobbyId { get; private set; }
            public bool WasSuccessful { get; private set; }

            public PlayerJoinedGameLobby(PlayerDto player, Guid lobbyId, bool wasSuccessful)
            {
                Player = player;
                LobbyId = lobbyId;
                WasSuccessful = wasSuccessful;
            }
        }

        public class PlayersInGameLobby
        {
            public Guid LobbyId { get; private set; }
            public IEnumerable<PlayerDto> Players { get; private set; }

            public PlayersInGameLobby(Guid lobbyId, IEnumerable<PlayerDto> players)
            {
                LobbyId = lobbyId;
                Players = players;
            }
        }

        public class CreateAndJoinGameLobbyResult
        {
            public GameLobbyDto GameLobby { get; }
            public bool Success { get; }

            public CreateAndJoinGameLobbyResult(GameLobbyDto gameLobby, bool success)
            {
                GameLobby = gameLobby;
                Success = success;
            }
        }

        public class GetGameLobbiesResult
        {
            public GetGameLobbiesResult(IEnumerable<GameLobbyDto> gameLobbies)
            {
                GameLobbies = gameLobbies;
            }

            public IEnumerable<GameLobbyDto> GameLobbies { get; }
        }

        public class JoinGameLobbyResult
        {
            public JoinGameLobbyResult(GameLobbyDto gameLobby, bool success)
            {
                GameLobby = gameLobby;
                Success = success;
            }

            public bool Success { get; }
            public GameLobbyDto GameLobby { get; }
        }

        public class IsAnyLobbyLookingForPlayersResult
        {
            public IsAnyLobbyLookingForPlayersResult(GameLobbyDto gameLobby, bool success)
            {
                GameLobby = gameLobby;
                Success = success;
            }

            public bool Success { get; }
            public GameLobbyDto GameLobby { get; }

        }

        public class PrepareGameScreen
        {
            public PrepareGameScreen(Guid gameSessionId)
            {
                GameSessionId = gameSessionId;
            }

            public Guid GameSessionId { get; }
        }

        public class StartGame
        {
        }
    }
}
