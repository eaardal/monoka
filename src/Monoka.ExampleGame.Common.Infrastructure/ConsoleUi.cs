using System;

namespace Monoka.ExampleGame.Common.Infrastructure
{
    class ConsoleUi : IUi
    {
        public void Write(params string[] lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}