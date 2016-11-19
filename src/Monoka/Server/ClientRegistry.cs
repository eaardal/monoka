using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Akka.Actor;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging.Contracts;

namespace Monoka.Server
{
    public class ClientRegistry : LoggingReceiveActor
    {
        private ImmutableList<ClientDto> _clients;

        public ClientRegistry(ILogger log) : base(log)
        {
            _clients = ImmutableList<ClientDto>.Empty;

            Receive<NewClient>(msg => OnNewClient(msg));
            Receive<GetClient>(msg => Sender.Tell(_clients.SingleOrDefault(c => c.AssignedId == msg.ClientId), Self));
            Receive<GetClients>(msg => OnGetClients(msg));
        }

        private void OnGetClients(GetClients msg)
        {
            var clients = msg.ClientIds.Any() 
                ? _clients.Where(c => msg.ClientIds.Contains(c.AssignedId)) 
                : _clients;

            Sender.Tell(clients, Self);
        }

        private void OnNewClient(NewClient msg)
        {
            var client = new ClientDto
            {
                Username = msg.PlayerName,
                Accepted = true,
                ActorSystemAddress = msg.Sender.Path.Address.ToString(),
                AssignedId = Guid.NewGuid(),
                Timestamp = DateTime.Now,
                Reason = null
            };
            
            Log.Msg(this, l => l.Debug(client.ActorSystemAddress));

            _clients = _clients.Add(client);

            Sender.Tell(client, Self);
        }

        #region Messages

        internal class NewClient
        {
            public string PlayerName { get; }
            public IActorRef Sender { get; }

            public NewClient(string playerName, IActorRef sender)
            {
                PlayerName = playerName;
                Sender = sender;
            }
        }

        internal class GetClient
        {
            public GetClient(Guid clientId)
            {
                ClientId = clientId;
            }

            public Guid ClientId { get; }
        }

        internal class GetClients
        {
            public GetClients(IEnumerable<Guid> clientIds)
            {
                ClientIds = clientIds ?? new Guid[0];
            }

            public GetClients(params Guid[] clientIds)
            {
                ClientIds = clientIds ?? new Guid[0];
            }

            public IEnumerable<Guid> ClientIds { get; }
        }

        #endregion
    }
}
