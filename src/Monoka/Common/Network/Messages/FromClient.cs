using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monoka.Common.Dto;

namespace Monoka.Common.Network.Messages
{
    public class FromClient
    {
        public class Handshake
        {
            public string Username { get; private set; }

            public Handshake(string username)
            {
                Username = username;
            }
        }

        public class LogIn
        {
            public string Username { get; private set; }
            public DateTime Timestamp { get; private set; }

            public LogIn(string username, DateTime timestamp)
            {
                Username = username;
                Timestamp = timestamp;
            }
        }

        public class PlayerReady
        {
            public Guid LobbyId { get; private set; }
            public Guid PlayerId { get; private set; }

            public PlayerReady(Guid playerId, Guid lobbyId)
            {
                LobbyId = lobbyId;
                PlayerId = playerId;
            }
        }

        public class GetGameLobbies
        {

        }

        public class CreateAndJoinGameLobby
        {
            public CreateAndJoinGameLobby(Guid playerId, string gameLobbyName)
            {
                PlayerId = playerId;
                GameLobbyName = gameLobbyName;
            }

            public Guid PlayerId { get; private set; }
            public string GameLobbyName { get; private set; }
        }

        public class JoinGameLobby
        {
            public JoinGameLobby(Guid playerId, Guid lobbyId)
            {
                PlayerId = playerId;
                LobbyId = lobbyId;
            }

            public Guid PlayerId { get; }
            public Guid LobbyId { get; }
        }

        public class RequestRefreshGameSession
        {
            public Guid GameSessionId { get; private set; }

            public RequestRefreshGameSession(Guid gameSessionId)
            {
                GameSessionId = gameSessionId;
            }
        }

        public class StartGame
        {
            public Guid GameLobbyId { get; private set; }

            public StartGame(Guid gameLobbyId)
            {
                GameLobbyId = gameLobbyId;
            }
        }

        public class PlayerAction
        {
            public PlayerAction(QueueEntryDto queueEntry)
            {
                QueueEntry = queueEntry;
            }

            public QueueEntryDto QueueEntry { get; private set; }
        }

        public class IsAnyLobbyLookingForPlayers { }

        public class DonePreparing
        {
            public Guid PlayerId { get; }
            public Guid GameSessionId { get; }

            public DonePreparing(Guid playerId, Guid gameSessionId)
            {
                PlayerId = playerId;
                GameSessionId = gameSessionId;
            }
        }
    }
}
