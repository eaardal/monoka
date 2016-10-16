using Monoka.ExampleGame.Common.Infrastructure;

namespace Monoka.ExampleGame.Common.Network
{
    public class RemoteActorRegistry
    {
        public class Server
        {
            public static RemoteActorMetadata RunCommandApi => new RemoteActorMetadata("commandDelegatorApi", ServerConnectionInfo.ActorSystemPath);

        }

        public class Client
        {
            
        }
    }
}
