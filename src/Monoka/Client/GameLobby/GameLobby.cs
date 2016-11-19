using System;
using System.Collections.Generic;
using Monoka.Client.Model;

namespace Monoka.Client.GameLobby
{
    public class GameLobby
    {
        public Guid Id { get; set; }
        public List<Player> Players { get; set; }
        public string Title { get; set; }

        public GameLobby()
        {
            Players = new List<Player>();
        }
    }
}
