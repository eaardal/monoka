using Monoka.Common.Network;

namespace Monoka.ExampleGame.Common.Network
{
    public class ServerAppConnectionInfo : ServerConnectionInfo
    {
        public override string ActorSystemName => "peon-server";
        public override string Hostname => "localhost";
        public override int Port => 8000;
        public override string Transport => "tcp";
    }
}