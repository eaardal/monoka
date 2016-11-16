using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Monoka.Common.Network;

namespace Monoka.Server.Startup
{
    public class AkkaBootstrapper
    {
        public static void Wire(IContainer container)
        {
            var serverConnectionInfo = container.Resolve<ServerConnectionInfo>();

            var system = CreateActorSystem(serverConnectionInfo);

            CreateAndRegisterActors(container, system);
        }

        private static void CreateAndRegisterActors(IContainer container, ActorSystem system)
        {
            var propsResolver = new AutoFacDependencyResolver(container, system);
            system.AddDependencyResolver(propsResolver);

            var builder = new ContainerBuilder();
            
            builder.RegisterInstance(system).AsImplementedInterfaces().AsSelf().SingleInstance();

            builder.Update(container);

            ActivateActors(system);
        }

        /// <summary>
        /// Activate certain actors on startup so that they may listen for messages immediately.
        /// </summary>
        /// <param name="system"></param>
        private static void ActivateActors(ActorSystem system)
        {
            //system.ActorOf(system.DI().Props<CommandDelegatorApi>(), RemoteActorRegistry.Server.RunCommandApi.Name);
            
            //var clientRegistry = system.ActorFromIoC(ActorRegistry.ClientRegistry);
            //var gameSessionManager = system.ActorFromIoC(ActorRegistry.GameSessionManager);
        }

        private static ActorSystem CreateActorSystem(ServerConnectionInfo serverConnection)
        {
            var config = GetConfig(serverConnection);

            return ActorSystem.Create(serverConnection.ActorSystemName, config);
        }

        private static Config GetConfig(ServerConnectionInfo serverConnection)
        {
            var port = serverConnection.Port;
            var host = serverConnection.Hostname;
            var transport = serverConnection.Transport;

            var config = ConfigurationFactory.ParseString(@"
akka {  
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
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
        loglevel = DEBUG
        loggers = [""Akka.Logger.Serilog.SerilogLogger, Akka.Logger.Serilog""]
        debug {
            receive = on
            autoreceive = on
            lifecycle = on
            event-stream = on
            unhandled = on
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
