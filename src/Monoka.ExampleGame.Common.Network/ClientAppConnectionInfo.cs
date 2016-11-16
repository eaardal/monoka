using Monoka.Common.Network;

namespace Monoka.ExampleGame.Common.Network
{
    public class ClientAppConnectionInfo : ClientConnectionInfo
    {
        public override string ActorSystemName => "peon-client";
        public override string Hostname => "localhost";
        public override int Port => 0;
        public override string Transport => "tcp";
    }
}
