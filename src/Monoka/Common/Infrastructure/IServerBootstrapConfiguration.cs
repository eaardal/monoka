using System;
using Monoka.Common.Network;

namespace Monoka.Common.Infrastructure
{
    public interface IServerBootstrapConfiguration : IBootstrapConfiguration
    {
        void ConfigureServerConnectionInfo(Action<ServerConnectionInfo> configureServerConnectionInfo);
    }
}