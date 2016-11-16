using System;
using Monoka.ExampleGame.Server.ConsoleClient.Startup;
using Peon.Server.ConsoleClient;

namespace Monoka.ExampleGame.Server.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerBootstrapper.Wire();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(AsciiText.ServerHeader);
            Console.ResetColor();

            Console.ReadLine();
        }
    }
}
