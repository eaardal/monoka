using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Monoka.Server.Startup;

namespace Monoka.ExampleGame.Server.ConsoleClient.Startup
{
    class MyBootstrapper
    {
        public static void Wire()
        {
            MonokaServerBootstrapper.Wire(cfg =>
            {
                cfg.ConfigureIoC(c =>
                {
                    c.RegisterAssemblyTypes(Assembly.GetCallingAssembly())
                        .AsImplementedInterfaces()
                        .AsSelf();
                });

                cfg.ConfigureMapping(c =>
                {
                    
                });
            });
        }
    }
}
