using Monoka.Common.Infrastructure;

namespace Monoka.Common.Network
{
    public class RemoteActorRegistry
    {
        public class Server
        {
            //public static RemoteActorMetadata LoginReceiver => new RemoteActorMetadata("loginApi", ServerConnectionInfo.ActorSystemPath);
            //public static RemoteActorMetadata GameLobbyReceiver => new RemoteActorMetadata("gameLobbyApi", ServerConnectionInfo.ActorSystemPath);
            public static RemoteActorMetadata GameSessionReceiver => new RemoteActorMetadata("gameSessionReceiver");
        }

        public class Client
        {
            //public static RemoteActorMetadata ServerConnector => new RemoteActorMetadata("serverConnector", ClientConnectionInfo.ActorSystemPath);
            //public static RemoteActorMetadata Lobby => new RemoteActorMetadata("lobby", ClientConnectionInfo.ActorSystemPath);
            //public static RemoteActorMetadata GameLobby => new RemoteActorMetadata("gameLobby", ClientConnectionInfo.ActorSystemPath);
            public static RemoteActorMetadata GameSessionReceiver => new RemoteActorMetadata("gameSessionReceiver");
            public static RemoteActorMetadata GameLobbyReceiver => new RemoteActorMetadata("gameLobbyReceiver");
        }
    }
}
