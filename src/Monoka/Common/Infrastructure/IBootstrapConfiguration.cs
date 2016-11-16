using System;
using Akka.Actor;
using Autofac;
using AutoMapper;

namespace Monoka.Common.Infrastructure
{
    public interface IBootstrapConfiguration
    {
        void ConfigureIoC(Action<ContainerBuilder> configureIoC);
        void ConfigureMapping(Action<IMapperConfigurationExpression> configureMapping);
        void ResolveActorsOnLoad(Action<ActorSystem> resolveActorsOnLoad);
    }
}