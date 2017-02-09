using System;
using Monoka.Client.Model;

namespace Monoka.Client
{
    public interface IPlayerRegistry
    {
        Player GetLocalPlayer();
        Guid GetLocalPlayerId();
        bool IsKnownPlayer(Guid playerId);
        void AddPlayer(Player player);
    }
}