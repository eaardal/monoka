using System;
using System.Reflection;
using Autofac;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging;
using Monoka.Common.Infrastructure.Logging.Contracts;

namespace Monoka.Client.Startup
{
    internal static class AutofacBootstrapper
    {
        public static IContainer ConfigureDependencies(ILogger logger, Action<ContainerBuilder> configureIoC)
        {
            var builder = new ContainerBuilder();

            var thisAssembly = Assembly.GetExecutingAssembly();
            
            builder.RegisterAssemblyTypes(thisAssembly)
                .Except<Logger>()
                .Except<IoC>()
                .Except<MessageBus>()
                .Except<Director>()
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.RegisterInstance(logger).As<ILogger>().SingleInstance();
            builder.RegisterType<IoC>().As<IIoC>().SingleInstance();
            builder.RegisterType<MessageBus>().As<IMessageBus>().SingleInstance();
            builder.RegisterType<Director>().AsSelf().AsImplementedInterfaces().SingleInstance();

            configureIoC?.Invoke(builder);

            return builder.Build();
        }
    }
}