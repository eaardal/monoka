using Monoka.Client.Model;

namespace Monoka.Client.Lobby.Results
{
    public class JoinGameLobbyResult
    {
        public bool Success { get; set; }
        public GameLobby GameLobby { get; set; }
    }
}