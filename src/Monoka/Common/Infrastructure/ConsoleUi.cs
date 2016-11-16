using System;

namespace Monoka.Common.Infrastructure
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