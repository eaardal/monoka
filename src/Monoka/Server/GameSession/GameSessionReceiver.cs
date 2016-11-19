using System;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Adapters;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network;
using Monoka.Common.Network.Messages;

namespace Monoka.Server.GameSession
{
    public class GameSessionReceiver :LoggingReceiveActor
    {
        private readonly IAutoMapperAdapter _mapper;

        public GameSessionReceiver(ILogger log, IAutoMapperAdapter mapper) : base(log)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;

            //Receive<FromClient.PlayerAction>(msg => OnPlayerAction(msg));
            Receive<GameSession.StartLoadingScreen>(msg => OnStartLoadingScreen(msg));
            Receive<FromClient.DonePreparing>(msg => OnDonePreparing(msg));
        }

        private void OnDonePreparing(FromClient.DonePreparing msg)
        {
            var gameSession = Context.System.ActorSelection(ActorRegistry.GameSession.PathWithArgs(msg.GameSessionId));

            var donePreparing = _mapper.Map<FromClient.DonePreparing, GameSession.PlayerFinishedLoading>(msg);

            gameSession.Tell(donePreparing);
        }

        private void OnStartLoadingScreen(GameSession.StartLoadingScreen msg)
        {
            foreach (var client in msg.Clients)
            {
                var actorPath = RemoteActorRegistry.Client.GameSessionReceiver.WithRemoteBasePath(client.ActorSystemAddress);

                Log.Msg(this, l => l.Debug($"Sending to {actorPath}"));

                var actor = Context.ActorSelection(actorPath);

                actor.Tell(new FromServer.PrepareGameScreen(msg.GameSessionId));
            }
        }

        //private void OnPlayerAction(FromClient.PlayerAction msg)
        //{
        //    var action = _mapper.Map<QueueEntryDto, QueueEntry>(msg.QueueEntry);

        //    var path = string.Format("user/gameSessionManager/gameSession_{0}/gameLoop_{0}/gameEngine_{0}/actionQueue_{0}", action.SessionId);

        //    var actionQueue = Context.ActorSelection(path);

        //    actionQueue.Tell(new Internal.EnqueueAction(action));
        //}
    }
}
