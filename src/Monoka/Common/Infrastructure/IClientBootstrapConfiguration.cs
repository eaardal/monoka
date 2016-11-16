using System;
using Monoka.Common.Network;

namespace Monoka.Common.Infrastructure
{
    public interface IClientBootstrapConfiguration : IBootstrapConfiguration
    {
        void ConfigureClientConnectionInfo(Action<ClientConnectionInfo> configureClientConnectionInfo);
    }
}