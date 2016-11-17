using System;
using System.Configuration;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Monoka.Common.Infrastructure;
using Monoka.Common.Network;

namespace Monoka.Client.Startup
{
    internal static class AkkaBootstrapper
    {
        public static void Wire(IContainer container, ClientBootstrapConfiguration clientBootstrapConfiguration)
        {
            var clientConnectionInfo = new ClientConnectionInfo();

            if (clientBootstrapConfiguration.ConfigureClientConnectionInfoAction == null)
            {
                throw new ConfigurationErrorsException("ClientConnectionInfo not set. Ensure \"MonokaClientBootstrapper.Wire(cfg => cfg.ConfigureClientConnectionInfo(--configuration here--))\" is called during initialization");
            }

            clientBootstrapConfiguration.ConfigureClientConnectionInfoAction(clientConnectionInfo);

            var system = CreateActorSystem(clientConnectionInfo);

            CreateAndRegisterActors(container, system);

            clientBootstrapConfiguration.ResolveActorsOnLoadAction?.Invoke(system);
        }

        private static void CreateAndRegisterActors(IContainer container, ActorSystem system)
        {
            var propsResolver = new AutoFacDependencyResolver(container, system);
            system.AddDependencyResolver(propsResolver);

            var builder = new ContainerBuilder();
            
            builder.RegisterInstance(system)
                .AsSelf()
                .As<ActorSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            
            builder.Update(container);

            ActivateActors(system);
        }

        /// <summary>
        /// Activate certain actors on startup so that they may listen for messages immediately.
        /// </summary>
        /// <param name="system"></param>
        private static void ActivateActors(ActorSystem system)
        {
            //var gameSessionApi = system.ActorOf(system.DI().Props<GameSessionReceiver>(), RemoteActorRegistry.Client.GameSessionReceiver.Name);
            //var gameLobbyApi = system.ActorOf(system.DI().Props<GameLobbyReceiver>(), RemoteActorRegistry.Client.GameLobbyReceiver.Name);
        }

        private static ActorSystem CreateActorSystem(ClientConnectionInfo clientConnectionInfo)
        {
            var config = GetConfig(clientConnectionInfo);

            return ActorSystem.Create(clientConnectionInfo.ActorSystemName, config);
        }

        private static Config GetConfig(ClientConnectionInfo clientConnectionInfo)
        {
            var port = clientConnectionInfo.Port;
            var host = clientConnectionInfo.Hostname;
            var transport = clientConnectionInfo.Transport;

            var config = ConfigurationFactory.ParseString(@"
akka {  
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote"",
        serializers {
            wire = ""Akka.Serialization.WireSerializer, Akka.Serialization.Wire""
        }
        serialization-bindings {
            ""System.Object"" = wire
        }
        task-dispatcher {
            type = TaskDispatcher
            throughput = 100
        }
    }    
    remote {
        helios.tcp {
            transport-class = ""Akka.Remote.Transport.Helios.HeliosTcpTransport, Akka.Remote""
		    applied-adapters = []
            transport-protocol = " + transport + @"
		    port = " + port + @"
		    hostname = " + host + @"
        }
    }
}
");
            return config;
        }
    }
}
