using System;

namespace Monoka.Server.GameLobby
{
    public class GameLobbyPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public bool IsAdmin { get; set; }
    }
}