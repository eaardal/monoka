using System;
using System.Reflection;
using Autofac;
using Autofac.Builder;
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
                .Except<IoC>(Singleton)
                .Except<MessageBus>(Singleton)
                .Except<Director>(Singleton)
                .Except<PlayerRegistry>(Singleton)
                .Except<SceneRenderer>(Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.RegisterInstance(logger).As<ILogger>().SingleInstance();
            
            configureIoC?.Invoke(builder);

            return builder.Build();
        }

        private static void Singleton<T>(IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder)
        {
            builder.AsSelf().AsImplementedInterfaces().SingleInstance();
        }
    }
}