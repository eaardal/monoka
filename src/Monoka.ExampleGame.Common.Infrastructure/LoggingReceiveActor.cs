using System;
using Akka.Actor;
using Monoka.ExampleGame.Common.Infrastructure.Logging.Contracts;

namespace Monoka.ExampleGame.Common.Infrastructure
{
    public abstract class LoggingReceiveActor : ReceiveActor
    {
        protected ILogger Log { get; }

        protected LoggingReceiveActor(ILogger log)
        {
            if (log == null) throw new ArgumentNullException(nameof(log));
            Log = log;
        }

        protected override bool AroundReceive(Receive receive, object message)
        {
            Log.Msg(this, l => l.Debug("{0} received {1}", GetType().Name, message.GetType().Name));

            return base.AroundReceive(receive, message);
        }

        protected override void Unhandled(object message)
        {
            Log.Msg(this, l => l.Warning("{0} received Unhandled {1}", GetType().Name, message.GetType().Name));

            base.Unhandled(message);
        }

        protected void LogFailure(object answer)
        {
            if (answer is Failure)
            {
                var failure = answer as Failure;
                Log.Msg(this, l => l.Error(failure.Exception));
                Sender.Tell(failure, Self);
            }
        }

        protected void LogException(Exception ex)
        {
            Log.Msg(this, l => l.Error(ex));
            Sender.Tell(new Failure { Exception = ex }, Self);
        }
    }
}
