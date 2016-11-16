using System;
using Monoka.Common.Network;

namespace Monoka.Common.Infrastructure
{
    public class ClientBootstrapConfiguration : BootstrapConfiguration, IClientBootstrapConfiguration
    {
        public Action<ClientConnectionInfo> ConfigureClientConnectionInfoAction { get; private set; }

        public void ConfigureClientConnectionInfo(Action<ClientConnectionInfo> configureClientConnectionInfo)
        {
            ConfigureClientConnectionInfoAction = configureClientConnectionInfo;
        }
    }
}