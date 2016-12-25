using System.Linq;
using System.Reflection;
using Autofac;
using Monoka.Client;
using Monoka.Client.Startup;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.ExampleGame.Client.Scenes.InGame.Screens;
using Monoka.ExampleGame.Client.Scenes.Menu.Screens.ExitGame;
using Monoka.ExampleGame.Client.Scenes.Menu.Screens.Menu;

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

                    thisAssemblyTypes
                        .Where(type => type.IsSubclassOf(typeof(Scene)))
                        .ForEach(type => builder.RegisterType(type).AsSelf().As<IScene>().Named<IScene>(type.FullName).SingleInstance());

                    // Game
                    builder.RegisterType<GameScreen>().AsSelf().SingleInstance();
                    builder.RegisterType<MenuScreen>().AsSelf().SingleInstance();
                    builder.RegisterType<ExitGameScreen>().AsSelf().SingleInstance();
                    builder.RegisterType<MenuItemFactory>().AsSelf().SingleInstance();

                    // Monoka (TODO: Move)
                    builder.RegisterType<ScreenManager>().AsSelf().SingleInstance();

                    if (game != null)
                    {
                        builder.RegisterInstance(game.Graphics).AsSelf().SingleInstance();
                        builder.RegisterInstance(game.GraphicsDevice).AsSelf().SingleInstance();
                        builder.RegisterInstance(game.Content).AsSelf().SingleInstance();
                    }
                });
            });
        }
    }
}
