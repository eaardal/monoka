using System.Collections.Generic;
using Akka.Actor;

namespace Monoka.Server.GameSession
{
    public class GameSessionManager : LoggingReceiveActor
    {
        private readonly List<IActorRef> _gameSessions;

        public GameSessionManager(ILogger log) : base(log)
        {
            _gameSessions = new List<IActorRef>();

            Receive<NewSession>(msg => OnNewSession(msg));
        }

        private void OnNewSession(NewSession msg)
        {
            var session = Context.ActorFromIoC<GameSession>(ActorRegistry.GameSession.NameWithArgs(msg.GameLobby.Id));

            Log.Msg(this, l => l.Info($"Session actor created {session.Path.ToString()} | {session.Path.Address.ToString()}"));

            session.Tell(new GameSession.CreateAndStartSession(msg.GameLobby), Self);

            _gameSessions.Add(session);
        }

        #region Messages

        public class NewSession
        {
            public GameLobby.GameLobby GameLobby { get; }

            public NewSession(GameLobby.GameLobby gameLobby)
            {
                GameLobby = gameLobby;
            }
        }
        
        #endregion
    }
}
