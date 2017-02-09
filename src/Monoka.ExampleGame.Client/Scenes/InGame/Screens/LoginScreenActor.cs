using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.ExampleGame.Common.Network;

namespace Monoka.ExampleGame.Client.Scenes.InGame.Screens
{
    class LoginScreenActor : LoggingReceiveActor
    {
        public LoginScreenActor(ILogger log) : base(log)
        {
            Become(Anonymous);
        }

        private void Anonymous()
        {
            Receive<LogIn>(msg => OnLogIn(msg));
        }

        private void OnLogIn(LogIn obj)
        {
            var loginReceiver = Context.ActorSelection(RemoteActorRegistry.Server.LoginReceiver);
            loginReceiver.Tell(new LoginReceiver.LogIn(obj));
        }

        #region Messages

        internal class LogIn
        {
        }

        #endregion
    }


}
