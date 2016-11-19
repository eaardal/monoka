using System;
using System.Collections.Generic;
using System.Linq;

namespace Monoka.Client.Model
{
    internal class PlayerRegistry
    {
        private readonly List<Player> _players;

        public PlayerRegistry()
        {
            _players = new List<Player>();
        }

        public Player GetLocalPlayer()
        {
            return _players.SingleOrDefault(player => player.IsLocalPlayer);
        }

        public bool IsKnownPlayer(Guid playerId)
        {
            return _players.Select(player => player.Id).Contains(playerId);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }
    }
}