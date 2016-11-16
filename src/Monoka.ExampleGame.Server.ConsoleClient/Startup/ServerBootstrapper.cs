using Monoka.Common.Infrastructure;
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
            });
            
            return ioc;
 ;       }
    }
}
