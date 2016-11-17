using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Monoka.Client;
using Monoka.Client.Startup;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;

namespace Monoka.ExampleGame.Client.Startup
{
    public static class ClientBootstrapper
    {
        public static IIoC Wire(GameLoop game)
        {
            return MonokaClientBootstrapper.Wire(config =>
            {
                config.ConfigureClientConnectionInfo(client =>
                {
                    client.ActorSystemName = "monoka-game-client";
                    client.Hostname = "localhost";
                    client.Port = 0; // OS assigns port
                    client.Transport = "tcp";
                });

                config.ConfigureIoC(builder =>
                {
                    var thisAssembly = Assembly.GetAssembly(typeof(ClientBootstrapper));
                    var thisAssemblyTypes = thisAssembly.GetTypes();

                    builder.RegisterTypes(thisAssemblyTypes)
                        .Where(type => !FilterScenes(type))
                        .AsSelf()
                        .AsImplementedInterfaces();

                    var scenes = thisAssemblyTypes
                        .Where(FilterScenes);

                    foreach (var scene in scenes)
                    {
                        builder.RegisterType(scene)
                            .AsSelf()
                            .As<IScene>()
                            .SingleInstance();
                    }

                    if (game != null)
                    {
                        builder.RegisterInstance(game.Graphics).AsSelf().SingleInstance();
                        builder.RegisterInstance(game.GraphicsDevice).AsSelf().SingleInstance();
                        builder.RegisterInstance(game.Content).AsSelf().SingleInstance();
                    }
                });
            });
        }

        private static bool FilterScenes(Type type)
        {
            return type.IsSubclassOf(typeof(Scene)) && type.IsClass;
        }
    }
}
