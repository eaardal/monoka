using System;

namespace Monoka.Client
{
    public interface IMatchmakerFacade
    {
        void FindGame(Guid playerId);
    }
}