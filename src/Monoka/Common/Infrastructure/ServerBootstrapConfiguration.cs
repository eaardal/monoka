using System;
using Monoka.Common.Network;

namespace Monoka.Common.Infrastructure
{
    public class ServerBootstrapConfiguration : BootstrapConfiguration, IServerBootstrapConfiguration
    {
        public Action<ServerConnectionInfo> ConfigureServerConnectionInfoAction { get; private set; }

        public void ConfigureServerConnectionInfo(Action<ServerConnectionInfo> configureServerConnectionInfo)
        {
            ConfigureServerConnectionInfoAction = configureServerConnectionInfo;
        }
    }
}