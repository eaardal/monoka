﻿using System;

namespace Monoka.Server.GameSession
{
    internal class GameSessionApi :LoggingReceiveActor
    {
        private readonly IAutoMapperAdapter _mapper;

        public GameSessionApi(ILogger log, IAutoMapperAdapter mapper) : base(log)
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
                var actorPath = RemoteActorRegistry.Client.GameSessionApi.WithRemoteBasePath(client.ActorSystemAddress);

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