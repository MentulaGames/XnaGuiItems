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
    using Core.Structs;
    using System;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class Extentions
    {
        public static Texture2D ApplyBorderLabel(this Texture2D texture, BorderStyle style)
        {
            Texture2D result = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            Color[] data = texture.GetColorData();

            Color ld, ru;
            switch (style)
            {
                case (BorderStyle.FixedSingle):
                    ru = Color.DimGray;
                    ld = Color.DimGray;
                    break;
                case (BorderStyle.Fixed3D):
                    ru = Color.DarkSlateGray;
                    ld = Color.White;
                    break;
                default:
                    return texture;
            }

            for (int x0 = 0, x1 = (texture.Height - 1) * texture.Width; x0 < texture.Width; x0++)
            {
                data[x0] = ru;
                data[x1 + x0] = ld;
            }

            for (int y0 = 0, y1 = texture.Width - 1; y0 < texture.Height; y0++)
            {
                data[y0 * texture.Width] = ru;
                data[y0 * texture.Width + y1] = ld;
            }

            result.SetData(data);
            return result;
        }

        public static Texture2D ApplyBorderButton(this Texture2D texture, ButtonStyle type, bool darkenOnClick)
        {
            Texture2D result = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            Color[] data = texture.GetColorData();

            Color bc;
            switch (type)
            {
                case ButtonStyle.Default:
                    bc = Color.CornflowerBlue;
                    break;
                case ButtonStyle.Hover:
                    bc = Color.DimGray;
                    break;
                case ButtonStyle.Click:
                    bc = Color.Black;

                    if (darkenOnClick)
                    {
                        for (int y = 1; y < texture.Height - 1; y++)
                        {
                            for (int x = 1; x < texture.Width - 1; x++)
                            {
                                int i = y * texture.Width + x;
                                data[i].R -= 10;
                                data[i].B -= 10;
                                data[i].G -= 10;
                            }
                        }
                    }
                    break;
                default:
                    return texture;
            }

            for (int x0 = 0, x1 = (texture.Height - 1) * texture.Width; x0 < texture.Width; x0++)
            {
                data[x0] = bc;
                data[x1 + x0] = bc;
            }

            for (int y0 = 0, y1 = texture.Width - 1; y0 < texture.Height; y0++)
            {
                data[y0 * texture.Width] = bc;
                data[y0 * texture.Width + y1] = bc;
            }

            result.SetData(data);
            return result;
        }

        public static Texture2D Clip(this Texture2D texture, Rectangle bounds)
        {
            Texture2D result = new Texture2D(texture.GraphicsDevice, bounds.Width, bounds.Height);
            Color[] prevData = texture.GetColorData();
            Color[] newData = new Color[bounds.Width * bounds.Height];

            for (int y = 0; y < bounds.Height; y++)
            {
                for (int x = 0; x < bounds.Width; x++)
                {
                    if (texture.Bounds.Contains(x, y)) newData[y * bounds.Width + x] = prevData[y * texture.Width + x];
                }
            }

            result.SetData(newData);
            return result;
        }

        public static Texture2D RenderOnto(this Texture2D texture, SpriteBatch sb, Size size, Vector2 position = default(Vector2), float rotation = 0, Vector2 scale = default(Vector2))
        {
            Texture2D result = new Texture2D(sb.GraphicsDevice, size.Width, size.Height);

            using (RenderTarget2D target = new RenderTarget2D(sb.GraphicsDevice, size.Width, size.Height))
            {
                sb.GraphicsDevice.SetRenderTarget(target);
                sb.GraphicsDevice.Clear(Color.Transparent);

                sb.Begin();
                sb.Draw(texture, position, null, Color.White, rotation, Vector2.Zero, scale == Vector2.Zero ? Vector2.One : scale, SpriteEffects.None, 1f);
                sb.End();

                sb.GraphicsDevice.SetRenderTarget(null);
                result.SetData(target.GetColorData());
            }

            return result;
        }

        public static Texture2D Stretch(this Texture2D texture, SpriteBatch sb, Size size)
        {
            Texture2D result = new Texture2D(texture.GraphicsDevice, size.Width, size.Height);

            using (RenderTarget2D target = new RenderTarget2D(texture.GraphicsDevice, size.Width, size.Height))
            {
                texture.GraphicsDevice.SetRenderTarget(target);
                texture.GraphicsDevice.Clear(Color.Transparent);

                sb.Begin();
                sb.Draw(texture, new Rectangle(0, 0, size.Width, size.Height), Color.White);
                sb.End();

                texture.GraphicsDevice.SetRenderTarget(null);
                result.SetData(target.GetColorData());
            }

            return result;
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ToPoint(this Vector2 vect)
        {
            return new Point((int)vect.X, (int)vect.Y);
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

        public static TKey[] GetKeys<TKey, TValue>(this KeyValuePair<TKey, TValue>[] kvp)
        {
            TKey[] result = new TKey[kvp.Length];
            for (int i = 0; i < kvp.Length; i++)
            {
                result[i] = kvp[i].Key;
            }
            return result;
        }
    }
}