#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems
{
#if MONO
    using Mono.Microsoft.Xna.Framework;
    using Mono.Microsoft.Xna.Framework.Graphics;
    using Mono.Microsoft.Xna.Framework.Input;
#else
    using Xna.Microsoft.Xna.Framework;
    using Xna.Microsoft.Xna.Framework.Graphics;
    using Xna.Microsoft.Xna.Framework.Input;
#endif
    using Core;
    using System;
    using System.Runtime.CompilerServices;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class Extentions
    {
        public static Texture2D ApplyBorderLabel(this Texture2D texture, BorderStyle style)
        {
            Texture2D newTexture = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            Color[] data = texture.GetColorData();

            switch (style)
            {
                case (BorderStyle.FixedSingle):
                    for (int y = 0; y < texture.Height; y++)
                    {
                        for (int x = 0; x < texture.Width; x++)
                        {
                            if (y == 0 || x == 0 || y == texture.Height - 1 || x == texture.Width - 1)
                            {
                                data[x + y * texture.Width] = Color.DimGray;
                            }
                        }
                    }
                    newTexture.SetData(data);
                    return newTexture;
                case (BorderStyle.Fixed3D):
                    for (int y = 0; y < texture.Height; y++)
                    {
                        for (int x = 0; x < texture.Width; x++)
                        {
                            if (y == 0 || x == 0) data[x + y * texture.Width] = Color.DarkSlateGray;
                            else if (y == texture.Height - 1 || x == texture.Width - 1) data[x + y * texture.Width] = Color.White;
                        }
                    }
                    newTexture.SetData(data);
                    return newTexture;
                default:
                    return texture;
            }
        }

        public static Texture2D ApplyBorderButton(this Texture2D texture, ButtonStyle type)
        {
            Texture2D newTexture = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            Color[] data = texture.GetColorData();

            switch (type)
            {
                case (ButtonStyle.Default):
                    for (int y = 0; y < texture.Height; y++)
                    {
                        for (int x = 0; x < texture.Width; x++)
                        {
                            if (y == 0 || x == 0 || y == texture.Height - 1 || x == texture.Width - 1)
                            {
                                data[x + y * texture.Width] = Color.CornflowerBlue;
                            }
                        }
                    }
                    newTexture.SetData(data);
                    return newTexture;
                case (ButtonStyle.Hover):
                    for (int y = 0; y < texture.Height; y++)
                    {
                        for (int x = 0; x < texture.Width; x++)
                        {
                            if (y == 0 || x == 0 || y == texture.Height - 1 || x == texture.Width - 1)
                            {
                                data[x + y * texture.Width] = Color.DimGray;
                            }
                        }
                    }
                    newTexture.SetData(data);
                    return newTexture;
                case (ButtonStyle.Click):
                    for (int y = 0; y < texture.Height; y++)
                    {
                        for (int x = 0; x < texture.Width; x++)
                        {
                            if (y == 0 || x == 0 || y == texture.Height - 1 || x == texture.Width - 1)
                            {
                                data[x + y * texture.Width] = Color.Black;
                            }
                            else data[x + y * texture.Width] = data[x + y * texture.Width].Add(0, -10, -10, -10);
                        }
                    }
                    newTexture.SetData(data);
                    return newTexture;
                default:
                    return texture;
            }
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
            RenderTarget2D target = new RenderTarget2D(sb.GraphicsDevice, size.Width, size.Height);

            sb.GraphicsDevice.SetRenderTarget(target);
            sb.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin();
            sb.Draw(texture, position, null, Color.White, rotation, Vector2.Zero, scale == default(Vector2) ? Vector2.One : scale, SpriteEffects.None, 1f);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(null);
            Texture2D result = new Texture2D(sb.GraphicsDevice, size.Width, size.Height);
            result.SetData(target.GetColorData());

            target.Dispose();

            return result;
        }

        public static Texture2D Stretch(this Texture2D texture, Size size)
        {
            RenderTarget2D target = new RenderTarget2D(texture.GraphicsDevice, size.Width, size.Height);
            SpriteBatch sb = new SpriteBatch(texture.GraphicsDevice);

            texture.GraphicsDevice.SetRenderTarget(target);
            texture.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin();
            sb.Draw(texture, new Rectangle(0, 0, size.Width, size.Height), Color.White);
            sb.End();

            texture.GraphicsDevice.SetRenderTarget(null);
            Texture2D result = new Texture2D(texture.GraphicsDevice, size.Width, size.Height);
            result.SetData(target.GetColorData());

            target.Dispose();
            sb.Dispose();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RequiresWork(this Anchor rPos)
        {
            return rPos != Anchor.None && rPos.IsValid();
        }

        public static bool IsValid(this Anchor rPos)
        {
            Anchor mask = Anchor.None;
            Func<bool> check = () => ContainesValue(rPos, mask);

            mask = Anchor.Bottom | Anchor.Top;
            if (check()) return false;

            mask = Anchor.Left | Anchor.Right;
            if (check()) return false;

            mask = Anchor.Left | Anchor.MiddleWidth;
            if (check()) return false;

            mask = Anchor.Right | Anchor.MiddleWidth;
            if (check()) return false;

            mask = Anchor.Top | Anchor.MiddelHeight;
            if (check()) return false;

            mask = Anchor.Bottom | Anchor.MiddelHeight;
            if (check()) return false;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainesValue(this Anchor anchor, Anchor mask)
        {
            return (anchor & mask) == mask;
        }

        public static Color Add(this Color c, int a, int r, int b, int g)
        {
            return new Color(c.R + r, c.G + g, c.B + b, c.A + a);
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
        public static int Clamp(this int value, int max, int min)
        {
            return Math.Min(max, Math.Max(min, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Position(this Rectangle rect)
        {
            return new Vector2(rect.X, rect.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Size(this Rectangle rect)
        {
            return new Size(rect.Width, rect.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ToPoint(this Vector2 vect)
        {
            return new Point((int)vect.X, (int)vect.Y);
        }

        public static Rectangle Add(this Rectangle rect, Vector2 position)
        {
            return new Rectangle(rect.X + (int)position.X, rect.Y + (int)position.Y, rect.Width, rect.Height);
        }

        public static string ToPassword(this string str, char passChar = '*')
        {
            return new string(passChar, str.Length);
        }

        public static float GetHeight(this SpriteFont font)
        {
            return font.MeasureString("Y").Y;
        }
    }
}