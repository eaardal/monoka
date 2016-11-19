using System;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network.Messages;

namespace Monoka.Server.NetworkApi
{
    public class LoginReceiver : LoggingReceiveActor
    {
        private readonly ActorSelection _clientRegistry;

        public LoginReceiver(ILogger log) : base(log)
        {
            _clientRegistry = Context.ActorSelection(ActorRegistry.ClientRegistry);

            ReceiveAsync<FromClient.Handshake>(async msg => await OnHandshake(msg));
        }

        private async Task OnHandshake(FromClient.Handshake msg)
        {
            try
            {
                var answer = await _clientRegistry.Ask(new ClientRegistry.NewClient(msg.Username, Sender));

                if (answer is ClientDto)
                {
                    var client = answer as ClientDto;

                    var player = new PlayerDto
                    {
                        Name = client.Username,
                        Id = client.AssignedId
                    };

                    Sender.Tell(new FromServer.HandshakeResult(true, player.Id), Self);
                }
            }
            catch (Exception ex)
            {
                Sender.Tell(new FromServer.HandshakeResult(false, Guid.Empty), Self);

                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }
    }
}
