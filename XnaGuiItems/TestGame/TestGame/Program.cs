using Mentula.Utilities;
using Mentula.Utilities.Logging;
using System;

namespace TestGame
{
#if WINDOWS || XBOX
    public static class Program
    {
        private static ConsoleLogger logger;

        public static void Main(string[] args)
        {
            logger = new ConsoleLogger() { AutoUpdate = true };
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.BufferHeight = short.MaxValue - 1;

            using (Game1 game = new Game1())
            {
                game.Run();
            }

            Utils.PressAnyKeyToContinue();
        }
    }
#endif
}

