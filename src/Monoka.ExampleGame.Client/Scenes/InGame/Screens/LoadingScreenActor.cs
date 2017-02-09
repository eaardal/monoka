using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging.Contracts;

namespace Monoka.ExampleGame.Client.Scenes.InGame.Screens
{
    class LoadingScreenActor : LoggingReceiveActor
    {
        public LoadingScreenActor(ILogger log) : base(log)
        {
            Become(StartingToLoad);
        }

        private void StartingToLoad()
        {
            Receive<StartLoadingLevel>(async msg => await OnStartLoadingLevel(msg));
        }

        private async Task OnStartLoadingLevel(StartLoadingLevel msg)
        {
            throw new NotImplementedException();
        }

        #region Messages

        internal class StartLoadingLevel
        {
        }

        #endregion
    }

}
