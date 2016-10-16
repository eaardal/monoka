using Autofac;
using Monoka.ExampleGame.Common.Infrastructure;
using Monoka.ExampleGame.Common.Infrastructure.Logging;
using Monoka.ExampleGame.Common.Infrastructure.Logging.LogFactories;

namespace Monoka.ExampleGame.Server.Startup
{
    public static class ServerAppBootstrapper
    {
        public static IIoC Wire()
        {
            var logger = new Logger();
            logger.InitializeLogFactories(new DebugLogFactory(), new SerilogLogFactory());
            Log.Initialize(logger);
            Log.Msg(typeof(ServerAppBootstrapper), log => log.Info("Log framework initialized"));

            var iocContainer = AutofacBootstrapper.ConfigureDependencies(logger);

            var ioc = iocContainer.Resolve<IIoC>();
            ioc.RegisterContainer(iocContainer);

            AutoMapperBootstrapper.Wire(iocContainer);

            AkkaBootstrapper.Wire(iocContainer);
            
            logger.Msg(typeof(ServerAppBootstrapper), l => l.Debug("Bootstrap configuration done"));

            return ioc;
        }
    }
}
