using System;
using Monoka.Client.Model;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network;
using Monoka.Common.Network.Messages;

namespace Monoka.Client.GameSession
{
    class GameSessionReceiver : LoggingReceiveActor
    {
        private readonly Director _director;
        private readonly PlayerRegistry _playerRegistry;

        public GameSessionReceiver(Director director, PlayerRegistry playerRegistry, ILogger log) : base(log)
        {
            if (director == null) throw new ArgumentNullException(nameof(director));
            if (playerRegistry == null) throw new ArgumentNullException(nameof(playerRegistry));
            _director = director;
            _playerRegistry = playerRegistry;

            Become(Awaiting);
        }

        private void Awaiting()
        {
            Receive<FromServer.PrepareGameScreen>(msg => OnStartLoadingScreen(msg));
        }

        private void Playing()
        {

        }

        private void Ready()
        {
            Receive<FromServer.StartGame>(msg => OnStartGame(msg));
        }

        private void OnStartLoadingScreen(FromServer.PrepareGameScreen msg)
        {
            _director.ActivateScene(KnownScene.LoadingScreen);

            var localPlayer = _playerRegistry.GetLocalPlayer();

            var serverGameSessionReceiver = Context.ActorSelection(RemoteActorRegistry.Server.GameSessionReceiver.Path);
            serverGameSessionReceiver.Tell(new FromClient.DonePreparing(localPlayer.Id, msg.GameSessionId));

            Become(Ready);
        }

        private void OnStartGame(FromServer.StartGame msg)
        {
            _director.ActivateScene(KnownScene.InGame);
            
            Become(Playing);
        }
    }
}
