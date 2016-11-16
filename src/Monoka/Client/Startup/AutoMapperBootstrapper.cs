using Autofac;
using AutoMapper;

namespace Monoka.Client.Startup
{
    internal static class AutoMapperBootstrapper
    {
        public static void Wire(IContainer container)
        {
            var config = new MapperConfiguration(Configure);

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            var builder = new ContainerBuilder();

            builder.RegisterInstance(mapper)
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Update(container);
        }

        private static void Configure(IMapperConfigurationExpression map)
        {
            
        }
    }
}
