using Akka.Actor;
using Akka.DI.Core;

namespace Monoka.Common.Infrastructure.Extensions
{
    public static class ActorSystemExtensions
    {
        public static ActorSelection ActorSelection(this ActorSystem actorSystem, ActorMetadata actor, ActorPathType path = ActorPathType.Absolute)
        {
            return actorSystem.ActorSelection(actor.Path);
        }
        
        public static ActorSelection ActorSelection(this IUntypedActorContext context, ActorMetadata actor, ActorPathType path = ActorPathType.Absolute)
        {
            return context.ActorSelection(actor.Path);
        }

        public static ActorSelection ActorSelection(this ActorSystem actorSystem, RemoteActorMetadata actor, ActorPathType path = ActorPathType.Absolute)
        {
            return actorSystem.ActorSelection(actor.RemotePath);
        }
        
        public static ActorSelection ActorSelection(this IUntypedActorContext context, RemoteActorMetadata actor, ActorPathType path = ActorPathType.Absolute)
        {
            return context.ActorSelection(actor.RemotePath);
        }

        public static IActorRef ActorOf(this IUntypedActorContext context, ActorMetadata actor)
        {
            return context.ActorOf(context.DI().Props(actor.ActorType), actor.Name);
        }

        public static IActorRef ActorFromIoC<T>(this IUntypedActorContext context, string name) where T : ActorBase
        {
            return context.ActorOf(context.DI().Props<T>(), name);
        }

        public static IActorRef ActorFromIoC<T>(this IUntypedActorContext context) where T : ActorBase
        {
            return context.ActorOf(context.DI().Props<T>());
        }

        public static IActorRef ActorFromIoC(this IUntypedActorContext context, ActorMetadata actor)
        {
            return context.ActorOf(context.DI().Props(actor.ActorType), actor.Name);
        }

        public static IActorRef ActorFromIoC<T>(this ActorSystem system, string name) where T : ActorBase
        {
            return system.ActorOf(system.DI().Props<T>(), name);
        }

        public static IActorRef ActorFromIoC<T>(this ActorSystem system) where T : ActorBase
        {
            return system.ActorOf(system.DI().Props<T>());
        }

        public static IActorRef ActorFromIoC(this ActorSystem system, ActorMetadata actor)
        {
            return system.ActorOf(system.DI().Props(actor.ActorType), actor.Name);
        }
    }
}
