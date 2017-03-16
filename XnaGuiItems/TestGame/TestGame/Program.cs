using Microsoft.Xna.Framework;
using System;

namespace TestGame
{
#if WINDOWS || XBOX
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.BufferHeight = short.MaxValue - 1;

            using (Game1 game = new Game1())
            {
                game.Run();
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
#endif
}

