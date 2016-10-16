using System;
using Monoka.ExampleGame.Server.Startup;
using Peon.Server.ConsoleClient;

namespace Monoka.ExampleGame.Server.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerAppBootstrapper.Wire();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(AsciiText.ServerHeader);
            Console.ResetColor();

            Console.ReadLine();
        }
    }
}
