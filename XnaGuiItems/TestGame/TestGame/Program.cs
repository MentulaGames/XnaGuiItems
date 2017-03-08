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

            using (Game1 game = new Game1())
            {
                game.Run();
            }

            Console.WriteLine("Press any ket to exit.");
            Console.ReadKey();
        }
    }
#endif
}

