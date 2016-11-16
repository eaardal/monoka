using System;
using Autofac;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging;
using Monoka.Common.Infrastructure.Logging.LogFactories;

namespace Monoka.Server.Startup
{
    public static class MonokaServerBootstrapper
    {
        public static IIoC Wire(Action<IBootstrapConfiguration> configureBootstrap)
        {
            var bootstrapConfiguration = new BootstrapConfiguration();
            configureBootstrap(bootstrapConfiguration);
            
            var logger = new Logger();
            logger.InitializeLogFactories(new DebugLogFactory(), new SerilogLogFactory());
            Log.Initialize(logger);
            Log.Msg(typeof(MonokaServerBootstrapper), log => log.Info("Log framework initialized"));

            var iocContainer = AutofacBootstrapper.ConfigureDependencies(logger, bootstrapConfiguration.ConfigureIoCAction);

            var ioc = iocContainer.Resolve<IIoC>();
            ioc.RegisterContainer(iocContainer);

            AutoMapperBootstrapper.Wire(iocContainer, bootstrapConfiguration.ConfigureMappingAction);

            AkkaBootstrapper.Wire(iocContainer, bootstrapConfiguration.ResolveActorsOnLoadAction);

            logger.Msg(typeof(MonokaServerBootstrapper), l => l.Debug("Monoka server bootstrap configuration done"));

            return ioc;
        }
    }
}

        