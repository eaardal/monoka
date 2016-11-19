using Akka.DI.Core;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Network;
using Monoka.Server.GameLobby;
using Monoka.Server.GameSession;
using Monoka.Server.NetworkApi;
using Monoka.Server.Startup;

namespace Monoka.ExampleGame.Server.ConsoleClient.Startup
{
    class ServerBootstrapper
    {
        public static IIoC Wire()
        {
            var ioc = MonokaServerBootstrapper.Wire(config =>
            {
                config.ConfigureServerConnectionInfo(server =>
                {
                    server.ActorSystemName = "monoka-game-server";
                    server.Hostname = "localhost";
                    server.Port = 8000;
                    server.Transport = "tcp";
                });

                config.ResolveActorsOnLoad(system =>
                {
                    system.ActorOf(system.DI().Props<LoginReceiver>(), RemoteActorRegistry.Server.LoginReceiver.Name);
                    system.ActorOf(system.DI().Props<GameLobbyReceiver>(), RemoteActorRegistry.Server.GameLobbyReceiver.Name);
                    system.ActorOf(system.DI().Props<GameSessionReceiver>(), RemoteActorRegistry.Server.GameSessionReceiver.Name);

                    system.ActorFromIoC(ActorRegistry.ClientRegistry);
                    system.ActorFromIoC(ActorRegistry.GameSessionManager);
                });
            });
            
            return ioc;
 ;       }
    }
}
