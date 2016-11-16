using System;
using Autofac;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging;
using Monoka.Common.Infrastructure.Logging.LogFactories;

namespace Monoka.Client.Startup
{
    public static class MonokaClientBootstrapper
    {
        public static IIoC Wire(Action<IClientBootstrapConfiguration> bootstrapConfiguration)
        {
            var bootstrapConfig = new ClientBootstrapConfiguration();
            bootstrapConfiguration(bootstrapConfig);

            var logger = new Logger();
            logger.InitializeLogFactories(new SerilogLogFactory());
            Log.Initialize(logger);
            Log.Msg(typeof(MonokaClientBootstrapper), log => log.Info("Log framework initialized"));
            
            var iocContainer = AutofacBootstrapper.ConfigureDependencies(logger, bootstrapConfig.ConfigureIoCAction);
            
            var ioc = iocContainer.Resolve<IIoC>();
            ioc.RegisterContainer(iocContainer);

            AutoMapperBootstrapper.Wire(iocContainer, bootstrapConfig.ConfigureMappingAction);

            AkkaBootstrapper.Wire(iocContainer, bootstrapConfig);
            
            logger.Msg(typeof(MonokaClientBootstrapper), l => l.Debug("Monoka client bootstrap configuration done"));
            
            return ioc;
        }
    }
}
