using System;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Client.EventQueue;
using Monoka.Client.Login.Results;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network;
using Monoka.Common.Network.Messages;

namespace Monoka.Client.Login
{
    public class LoginFacade : ActorFacade, ILoginFacade
    {
        private readonly IQueueBroker _queueBroker;

        public LoginFacade(ActorSystem actorSystem, IQueueBroker queueBroker, ILogger logger) : base(actorSystem, logger)
        {
            if (queueBroker == null) throw new ArgumentNullException(nameof(queueBroker));
            _queueBroker = queueBroker;
        }

        public void Handshake(string playerName)
        {
            var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.LoginReceiver.Path);

            var handshake = new FromClient.Handshake(playerName);

            actor.Tell(handshake);

            //if (answer is FromServer.HandshakeResult)
            //{
            //    var result = answer as FromServer.HandshakeResult;

            //    return new HandshakeResult { Success = result.Success, PlayerId = result.PlayerId };
            //}

            //LogFailure(answer);

            //return new HandshakeResult { Success = false, PlayerId = Guid.Empty };
        }
    }

        //public async Task<HandshakeResult> Handshake(string playerName)
        //{
        //    var actor = ActorSystem.ActorSelection(RemoteActorRegistry.Server.LoginReceiver.Path);

        //    var handshake = new FromClient.Handshake(playerName);

        //    var answer = await actor.Ask(handshake);

        //    if (answer is FromServer.HandshakeResult)
        //    {
        //        var result = answer as FromServer.HandshakeResult;

        //        return new HandshakeResult { Success = result.Success, PlayerId = result.PlayerId };
        //    }

        //    LogFailure(answer);

        //    return new HandshakeResult { Success = false, PlayerId = Guid.Empty };
        //}
    //}
}
