using Monoka.Client.Startup;
using Monoka.Common.Infrastructure;

namespace Monoka.ExampleGame.Client.Startup
{
    class ClientBootstrapper
    {
        public static IIoC Wire()
        {
            return MonokaClientBootstrapper.Wire(config =>
            {
                config.ConfigureClientConnectionInfo(client =>
                {
                    client.ActorSystemName = "monoka-game-client";
                    client.Hostname = "localhost";
                    client.Port = 0; // OS assigns port
                    client.Transport = "tcp";
                });
            });
        }
    }
}
