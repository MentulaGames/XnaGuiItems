#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
    using Mono::Microsoft.Xna.Framework.Graphics;
    using Mono::Microsoft.Xna.Framework.Input;
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
    using Xna::Microsoft.Xna.Framework.Input;
#endif
    using Core;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class Extentions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RequiresWork(this Anchor rPos)
        {
            return rPos != Anchor.None && rPos.IsValid();
        }

        public static bool IsValid(this Anchor rPos)
        {
            if (ContainesValue(rPos, Anchor.Bottom | Anchor.Top)) return false;
            if (ContainesValue(rPos, Anchor.Left | Anchor.Right)) return false;
            if (ContainesValue(rPos, Anchor.Left | Anchor.CenterWidth)) return false;
            if (ContainesValue(rPos, Anchor.Right | Anchor.CenterWidth)) return false;
            if (ContainesValue(rPos, Anchor.Top | Anchor.CenterHeight)) return false;
            if (ContainesValue(rPos, Anchor.Bottom | Anchor.CenterHeight)) return false;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainesValue(this Anchor anchor, Anchor mask)
        {
            return (anchor & mask) == mask;
        }

        public static Color[] GetColorData(this Texture2D texture)
        {
            Color[] value = new Color[texture.Width * texture.Height];
            texture.GetData(value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Position(this MouseState state)
        {
            return new Vector2(state.X, state.Y);
        }

        public static float GetHeight(this SpriteFont font)
        {
            return font.MeasureString("Y").Y;
        }

        public static void AddSet<Tkey, TVal>(this Dictionary<Tkey, TVal> dict, Tkey key, TVal val)
        {
            if (dict.ContainsKey(key)) dict[key] = val;
            else dict.Add(key, val);
        }
    }
}