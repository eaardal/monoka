using System;
using Akka.Actor;
using Autofac;
using AutoMapper;

namespace Monoka.Common.Infrastructure
{
    public class BootstrapConfiguration : IBootstrapConfiguration
    {
        public Action<ContainerBuilder> ConfigureIoCAction;
        public Action<IMapperConfigurationExpression> ConfigureMappingAction;
        public Action<ActorSystem> ResolveActorsOnLoadAction;
        
        public void ConfigureIoC(Action<ContainerBuilder> configureIoC)
        {
            ConfigureIoCAction = configureIoC;
        }

        public void ConfigureMapping(Action<IMapperConfigurationExpression> configureMapping)
        {
            ConfigureMappingAction = configureMapping;
        }

        public void ResolveActorsOnLoad(Action<ActorSystem> resolveActorsOnLoad)
        {
            ResolveActorsOnLoadAction = resolveActorsOnLoad;
        }
    }
}