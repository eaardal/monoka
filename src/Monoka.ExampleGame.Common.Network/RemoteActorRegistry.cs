using System.Security.Policy;
using Monoka.Common.Infrastructure;
using Monoka.Common.Network;

namespace Monoka.ExampleGame.Common.Network
{
    public class RemoteActorRegistry
    {
        public class Server
        {
            //public static RemoteActorMetadata RunCommandApi => new RemoteActorMetadata("commandDelegatorApi", ServerAppConnectionInfo.ActorSystemPath);
            public static RemoteActorMetadata LoginReceiver => new RemoteActorMetadata("loginReceiver", IoC.Instance.Resolve<ServerConnectionInfo>().ActorSystemPath);
        }

        public class Client
        {
            
        }
    }
}
