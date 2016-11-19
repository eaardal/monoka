using System;
using System.Collections.Generic;

namespace Monoka.Client.Model
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
