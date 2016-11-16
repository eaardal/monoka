using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monoka.ExampleGame.Client.Scenes.Menu;
using Monoka.ExampleGame.Client.Startup;
using NUnit.Framework;

namespace Monoka.ExampleGame.Client.UnitTests
{
    [TestFixture]
    public class ClientBootstrapperTests
    {
        [TestCase(typeof(MenuScene))]
        public void ResolvesType(Type type)
        {
            using (var game = new GameLoop())
            {
                var ioc = ClientBootstrapper.Wire(null);

                var instance = ioc.Resolve(type);

                Assert.NotNull(instance);
            }
        }
    }
}
