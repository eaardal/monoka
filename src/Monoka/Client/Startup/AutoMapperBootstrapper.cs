using System;
using Autofac;
using AutoMapper;

namespace Monoka.Client.Startup
{
    internal static class AutoMapperBootstrapper
    {
        public static void Wire(IContainer container, Action<IMapperConfigurationExpression> configureMapping)
        {
            var config = new MapperConfiguration(cfg =>
            {
                Configure(cfg);
                configureMapping(cfg);
            });

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
