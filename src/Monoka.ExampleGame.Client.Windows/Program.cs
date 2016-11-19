using System;
using Monoka.Client.Model;
using Monoka.Common.Infrastructure;

namespace Monoka.ExampleGame.Client.Windows
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            if (CompileConfigurationMode.IsDebug())
            {
                ConsoleWindow.Create();
            }

            using (var game = new GameLoop())
            {
                game.Run();
            }

            if (CompileConfigurationMode.IsDebug())
            {
                ConsoleWindow.Destroy();
            }
        }
    }
#endif
}
