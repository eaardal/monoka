using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging;
using Monoka.Common.Infrastructure.Logging.LogFactories;
using Monoka.Server.Startup;

namespace Monoka.ExampleGame.Server.ConsoleClient.Startup
{
    class ServerBootstrapper
    {
        public static IIoC Wire()
        {
            var logger = new Logger();
            logger.InitializeLogFactories(new DebugLogFactory(), new SerilogLogFactory());
            Log.Initialize(logger);
            Log.Msg(typeof(MonokaServerBootstrapper), log => log.Info("Log framework initialized"));

            var iocContainer = AutofacBootstrapper.ConfigureDependencies(logger);

            var ioc = iocContainer.Resolve<IIoC>();
            ioc.RegisterContainer(iocContainer);

            AutoMapperBootstrapper.Wire(iocContainer);

            AkkaBootstrapper.Wire(iocContainer);

            logger.Msg(typeof(MonokaServerBootstrapper), l => l.Debug("Bootstrap configuration done"));

            return ioc;
        }
    }
}
