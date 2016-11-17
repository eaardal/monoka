using System;
using System.Collections.Generic;
using System.Linq;

namespace Monoka.Server.GameLobby
{
    public class GameLobby
    {
        public GameLobby()
        {
            Players = new List<GameLobbyPlayer>();
            RequiredNrOfPlayers = 1;
            Title = "New Game Lobby";
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public List<GameLobbyPlayer> Players { get; set; }

        public string Title { get; set; }
        public static int RequiredNrOfPlayers { get; set; }
        public bool AreAllPlayersReady => Players.All(p => p.IsReady);

        public void Join(GameLobbyPlayer player)
        {
            Players.Add(player);
        }

        public void Leave(Guid playerId)
        {
            var player = Players.SingleOrDefault(p => p.Id == playerId);

            if (player != null)
            {
                Players.Remove(player);
            }
        }
    }
}
