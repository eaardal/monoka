using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Monoka.ExampleGame.Common.Network;

namespace Monoka.ExampleGame.Client.Startup
{
    public class AkkaBootstrapper
    {
        public static void Wire(IContainer container)
        {
            var system = CreateActorSystem();

            CreateAndRegisterActors(container, system);
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
            //var gameSessionApi = system.ActorOf(system.DI().Props<GameSessionApi>(), RemoteActorRegistry.Client.GameSessionApi.Name);
            //var gameLobbyApi = system.ActorOf(system.DI().Props<GameLobbyApi>(), RemoteActorRegistry.Client.GameLobbyApi.Name);
        }

        private static ActorSystem CreateActorSystem()
        {
            var config = GetConfig();

            return ActorSystem.Create(ClientConnectionInfo.ActorSystemName, config);
        }

        private static Config GetConfig()
        {
            const int port = ClientConnectionInfo.Port;
            const string host = ClientConnectionInfo.Hostname;
            const string transport = ClientConnectionInfo.Transport;

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
