using Akka.Actor;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging.Contracts;

namespace Monoka.Client.GameSession
{
    public class GameSessionFacade : ActorFacade
    {
        public GameSessionFacade(ActorSystem actorSystem, ILogger logger) : base(actorSystem, logger)
        { }

       
    }
}
