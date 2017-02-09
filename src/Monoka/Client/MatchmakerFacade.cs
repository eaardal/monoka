using System;
using Akka.Actor;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Server;

namespace Monoka.Client
{
    public class MatchmakerFacade : ActorFacade, IMatchmakerFacade
    {
        public MatchmakerFacade(ActorSystem actorSystem, ILogger logger) : base(actorSystem, logger)
        {
        }

        public void FindGame(Guid playerId)
        {
            var matchmaker = ActorSystem.ActorFromIoC(ActorRegistry.Matchmaker);

            matchmaker.Tell(new Matchmaker.FindGame(playerId));
        }
    }
}
