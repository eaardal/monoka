using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monoka.Client.Startup;
using Monoka.Common.Infrastructure;

namespace Monoka.ExampleGame.Client
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
