using Monoka.Common.Dto;

namespace Monoka.Client.GameLobby.Results
{
    public class LookingForPlayersResult
    {
        public LookingForPlayersResult(GameLobbyDto gameLobby, bool success)
        {
            GameLobby = gameLobby;
            Success = success;
        }

        public bool Success { get; }
        public GameLobbyDto GameLobby { get; }
    }
}