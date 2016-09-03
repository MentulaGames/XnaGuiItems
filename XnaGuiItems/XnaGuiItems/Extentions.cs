using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.CompilerServices;

namespace Mentula.GuiItems
{
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class Extentions
    {
        internal static Texture2D ApplyBorderLabel(this Texture2D texture, BorderStyle style)
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

        internal static Texture2D ApplyBorderButton(this Texture2D texture, ButtonStyle type)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool RequiresWork(this Anchor rPos)
        {
            return rPos != Anchor.None && rPos.IsValid(); 
        }

        internal static bool IsValid(this Anchor rPos)
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
        internal static bool ContainesValue(this Anchor anchor, Anchor mask)
        {
            return (anchor & mask) == mask;
        }

        internal static Color Add(this Color c, int a, int r, int b, int g)
        {
            return new Color(c.R + r, c.G + g, c.B + b, c.A + a);
        }

        internal static Color[] GetColorData(this Texture2D texture)
        {
            Color[] value = new Color[texture.Width * texture.Height];
            texture.GetData(value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector2 Position(this MouseState state)
        {
            return new Vector2(state.X, state.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int Clamp(this int value, int max, int min)
        {
            return value > max ? max : (value < min ? min : value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector2 Position(this Rectangle rect)
        {
            return new Vector2(rect.X, rect.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Size Size(this Rectangle rect)
        {
            return new Size(rect.Width, rect.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Point ToPoint(this Vector2 vect)
        {
            return new Point((int)vect.X, (int)vect.Y);
        }

        internal static Rectangle Add(this Rectangle rect, Vector2 position)
        {
            return new Rectangle(rect.X + (int)position.X, rect.Y + (int)position.Y, rect.Width, rect.Height);
        }

        internal static string ToPassword(this string str, char passChar = '*')
        {
            return new string(passChar, str.Length);
        }
    }
}