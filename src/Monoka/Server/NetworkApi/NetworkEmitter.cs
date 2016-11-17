using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;

namespace Monoka.Server.NetworkApi
{
    class NetworkEmitter : LoggingReceiveActor
    {
        private readonly ActorSelection _clientRegistry;

        public NetworkEmitter(ILogger log) : base(log)
        {
            _clientRegistry = Context.ActorSelection(ActorRegistry.ClientRegistry);

            ReceiveAsync<TellClients>(async msg => await OnTellAllClients(msg));
        }

        private async Task OnTellAllClients(TellClients msg)
        {
            try
            {
                var answer = await _clientRegistry.Ask(new ClientRegistry.GetClients());

                if (answer is IEnumerable<ClientDto>)
                {
                    var clients = answer as IEnumerable<ClientDto>;

                    foreach (var client in clients.Where(c => msg.ClientsToTell.Contains(c.AssignedId)))
                    {
                        var actorPath = msg.RemoteActor.WithRemoteBasePath(client.ActorSystemAddress);

                        var actor = Context.ActorSelection(actorPath);
                        
                        Log.Msg(this, l => l.Debug("{0} sending {1} to {2}", GetType().Name, msg.Message.GetType().Name, actorPath));

                        actor.Tell(msg.Message);
                    }
                }

                if (answer is Failure)
                {
                    var failure = answer as Failure;
                    Log.Msg(this, l => l.Error(failure.Exception));
                    Sender.Tell(failure, Self);
                }
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        public class TellClients
        {
            public object Message { get; }
            public IEnumerable<Guid> ClientsToTell { get; }
            public RemoteActorMetadata RemoteActor { get; }

            public TellClients(object message, IEnumerable<Guid> clientsToTell, RemoteActorMetadata remoteActor)
            {
                Message = message;
                ClientsToTell = clientsToTell;
                RemoteActor = remoteActor;
            }
        }
    }
}
