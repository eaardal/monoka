using Monoka.Common.Infrastructure;
using Monoka.Server.GameLobby;
using Monoka.Server.GameSession;
using Monoka.Server.Model;
using Monoka.Server.NetworkApi;

namespace Monoka.Server
{
    static class ActorRegistry
    {
        public static ActorMetadata ClientRegistry => new ActorMetadata("clientRegistry", typeof(ClientRegistry));
        public static ActorMetadata GameLobbyManager => new ActorMetadata("gameLobbyManager", typeof(GameLobbyManager));
        public static ActorMetadata GameSessionManager => new ActorMetadata("gameSessionManager", typeof(GameSessionManager));

        public static ActorMetadata GameSession => new ActorMetadata("gameSession_{0}", typeof(GameSession.GameSession), GameSessionManager);
        //public static ActorMetadata GameEngine => new ActorMetadata("gameEngine", typeof(GameEngine.GameEngine));
        public static ActorMetadata GameLobbyEmitter => new ActorMetadata("gameLobbyEmitter", typeof(GameLobbyEmitter));
        //public static ActorMetadata Bank => new ActorMetadata(typeof(Bank));
        //public static ActorMetadata PowerBar => new ActorMetadata(typeof(PowerBar));
        //public static ActorMetadata BuildingRegistry => new ActorMetadata(typeof(BuildingRegistry));

        //public static ActorMetadata GameSessionReceiver => new ActorMetadata("gameSessionApi", typeof(GameSessionReceiver));
    }
}
