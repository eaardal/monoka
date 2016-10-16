using Autofac;
using Monoka.ExampleGame.Common.Infrastructure;
using Monoka.ExampleGame.Common.Infrastructure.Logging;
using Monoka.ExampleGame.Common.Infrastructure.Logging.LogFactories;

namespace Monoka.ExampleGame.Client.Startup
{
    public static class ClientAppBootstrapper
    {
        public static IIoC Configure()
        {
            var logger = new Logger();
            logger.InitializeLogFactories(new DebugLogFactory(), new SerilogLogFactory());
            Log.Initialize(logger);
            Log.Msg(typeof(ClientAppBootstrapper), log => log.Info("Log framework initialized"));
            
            var iocContainer = AutofacBootstrapper.ConfigureDependencies(logger);
            
            var ioc = iocContainer.Resolve<IIoC>();
            ioc.RegisterContainer(iocContainer);

            AutoMapperBootstrapper.Wire(iocContainer);

            AkkaBootstrapper.Wire(iocContainer);
            
            logger.Msg(typeof(ClientAppBootstrapper), l => l.Debug("Bootstrap configuration done"));
            
            return ioc;
        }
    }
}
