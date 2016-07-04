using Microsoft.Xna.Framework;
using System;

namespace TestGame
{
    public static class Utils
    {
        private static readonly Random rng = new Random();

        public static T[] Populate<T>(this T[] coll, T value)
        {
            for (int i = 0; i < coll.Length; i++)
            {
                coll[i] = value;
            }

            return coll;
        }

        public static Color RndColor()
        {
            int r = rng.Next(byte.MaxValue);
            int g = rng.Next(byte.MaxValue);
            int b = rng.Next(byte.MaxValue);

            return new Color(r, g, b);
        }
    }
}