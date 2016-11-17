using System;

namespace Monoka.Server.GameLobby
{
    public class GameLobbyPlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public bool IsAdmin { get; set; }
    }
}