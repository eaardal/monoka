using System;
using System.Collections.Generic;
using System.Linq;
using Monoka.Client.Model;
using Monoka.Common.Infrastructure.Exceptions;

namespace Monoka.Client
{
    public class PlayerRegistry : IPlayerRegistry
    {
        private readonly List<Player> _players;

        public PlayerRegistry()
        {
            _players = new List<Player>();
        }

        public Player GetLocalPlayer()
        {
            var localPlayer = _players.SingleOrDefault(player => player.IsLocalPlayer);

            if (localPlayer == null)
            {
                throw new MonokaException($"No local player found in {nameof(PlayerRegistry)}");
            }

            return localPlayer;
        }

        public Guid GetLocalPlayerId()
        {
            var localPlayer = GetLocalPlayer();
            return localPlayer.Id;
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