using System;
using Monoka.ExampleGame.Server.ConsoleClient.Startup;
using Monoka.Server.Startup;

namespace Monoka.ExampleGame.Server.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerBootstrapper.Wire();
            
            Console.ReadLine();
        }
    }
}
