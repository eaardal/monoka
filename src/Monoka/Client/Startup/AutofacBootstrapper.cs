using System;
using System.Linq;
using System.Reflection;
using Akka.Util.Internal;
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
            //var referencedAssemblies = thisAssembly.GetReferencedAssemblies().Where(a => a.Name.StartsWith("Monoka"));
            //var appAssemblies = referencedAssemblies.Select(Assembly.Load).Concat(new[] { thisAssembly }).ToArray();

            builder.RegisterAssemblyTypes(thisAssembly)
                .Except<Logger>()
                .Except<IoC>()
                .Except<MessageBus>()
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.RegisterType<IoC>().As<IIoC>().SingleInstance();
            builder.RegisterType<MessageBus>().As<IMessageBus>().SingleInstance();
            builder.RegisterInstance(logger).As<ILogger>().SingleInstance();
            
            configureIoC?.Invoke(builder);

            return builder.Build();
        }
    }
}