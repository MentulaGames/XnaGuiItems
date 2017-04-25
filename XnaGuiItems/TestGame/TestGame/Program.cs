using DeJong.Utilities;
using DeJong.Utilities.Logging;

namespace TestGame
{
#if WINDOWS || XBOX
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (ConsoleLogger logger = new ConsoleLogger { AutoUpdate = true })
            {
                using (Game1 game = new Game1())
                {
                    game.Run();
                }
            }

            Utils.PressAnyKeyToContinue();
        }
    }
#endif
}