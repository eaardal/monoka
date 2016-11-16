using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Logging;
using Monoka.Common.Infrastructure.Logging.Contracts;

namespace Monoka.Server.Startup
{
    internal static class AutofacBootstrapper
    {
        public static IContainer ConfigureDependencies(ILogger logger, Action<ContainerBuilder> configureIoC)
        {
            var builder = new ContainerBuilder();

            var thisAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = thisAssembly.GetReferencedAssemblies().Where(a => a.Name.StartsWith("Monoka"));
            var appAssemblies = referencedAssemblies.Select(Assembly.Load).Concat(new[] { thisAssembly }).ToArray();

            builder.RegisterAssemblyTypes(appAssemblies)
                .Except<Logger>()
                .Except<IoC>()
                //.Except<MessageBus>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterType<Logger>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterType<IoC>().As<IIoC>().SingleInstance();
            builder.RegisterInstance(logger).As<ILogger>().SingleInstance();

            configureIoC?.Invoke(builder);

            return builder.Build();
        }
    }
}
