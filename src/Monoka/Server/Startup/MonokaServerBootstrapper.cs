using System;
using Autofac;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging;
using Monoka.Common.Infrastructure.Logging.LogFactories;

namespace Monoka.Server.Startup
{
    public static class MonokaServerBootstrapper
    {
        public static IIoC Wire(Action<IServerBootstrapConfiguration> bootstrapConfiguration = null)
        {
            WriteServerAsciiText();

            var bootstrapConfig = new ServerBootstrapConfiguration();
            bootstrapConfiguration?.Invoke(bootstrapConfig);
            
            var logger = new Logger();
            logger.InitializeLogFactories(new DebugLogFactory(), new SerilogLogFactory());
            Log.Initialize(logger);
            Log.Msg(typeof(MonokaServerBootstrapper), log => log.Info("Log framework initialized"));

            var iocContainer = AutofacBootstrapper.ConfigureDependencies(logger, bootstrapConfig.ConfigureIoCAction);

            var ioc = iocContainer.Resolve<IIoC>();
            ioc.RegisterContainer(iocContainer);

            AutoMapperBootstrapper.Wire(iocContainer, bootstrapConfig.ConfigureMappingAction);

            AkkaBootstrapper.Wire(iocContainer, bootstrapConfig);

            logger.Msg(typeof(MonokaServerBootstrapper), l => l.Debug("Monoka server bootstrap configuration done"));
            
            return ioc;
        }

        private static void WriteServerAsciiText()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(AsciiText.ServerText);
            Console.ResetColor();
        }
    }
}

        