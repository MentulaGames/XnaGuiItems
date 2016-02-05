using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mentula.GuiItems.Core;

namespace Mentula.GuiItems
{
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
                                data[x + y * texture.Width] = Color.DimGray;
                        }
                    }
                    newTexture.SetData<Color>(data);
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
                    newTexture.SetData<Color>(data);
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
                                data[x + y * texture.Width] = Color.CornflowerBlue;
                        }
                    }
                    newTexture.SetData<Color>(data);
                    return newTexture;
                case (ButtonStyle.Hover):
                    for (int y = 0; y < texture.Height; y++)
                    {
                        for (int x = 0; x < texture.Width; x++)
                        {
                            if (y == 0 || x == 0 || y == texture.Height - 1 || x == texture.Width - 1)
                                data[x + y * texture.Width] = Color.DimGray;
                        }
                    }
                    newTexture.SetData<Color>(data);
                    return newTexture;
                case (ButtonStyle.Click):
                    for (int y = 0; y < texture.Height; y++)
                    {
                        for (int x = 0; x < texture.Width; x++)
                        {
                            if (y == 0 || x == 0 || y == texture.Height - 1 || x == texture.Width - 1)
                                data[x + y * texture.Width] = Color.Black;
                            else data[x + y * texture.Width] = data[x + y * texture.Width].Change(0, -10, -10, -10);
                        }
                    }
                    newTexture.SetData<Color>(data);
                    return newTexture;
                default:
                    return texture;
            }
        }

        internal static Color Change(this Color c, int a, int r, int b, int g)
        {
            try { return new Color(c.R + r, c.G + g, c.B + b, c.A + a); }
            catch (Exception) { return c; }
        }

        internal static Color[] GetColorData(this Texture2D texture)
        {
            Color[] value = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(value);
            return value;
        }

        internal static Vector2 Position(this MouseState state)
        {
            return new Vector2(state.X, state.Y);
        }

        internal static Vector2 Position(this Rectangle rect)
        {
            return new Vector2(rect.X, rect.Y);
        }

        internal static Vector2 Size(this Rectangle rect)
        {
            return new Vector2(rect.Width, rect.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int X(this Vector2 vect)
        {
            return (int)vect.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int Y(this Vector2 vect)
        {
            return (int)vect.Y;
        }

        internal static Point ToPoint(this Vector2 vect)
        {
            return new Point(vect.X(), vect.Y());
        }

        internal static Rectangle Add(this Rectangle rect, Vector2 position)
        {
            return new Rectangle(rect.X + position.X(), rect.Y + position.Y(), rect.Width, rect.Height);
        }

        internal static Rectangle FromPosition(Vector2 position, int width, int height)
        {
            return new Rectangle(position.X(), position.Y(), width, height);
        }

        internal static string ToPassword(this string str, char passChar = '*')
        {
            string val = "";
            str.ToCharArray().ForEach(c => val += passChar);
            return val;
        }

        internal static void ForEach<T>(this IList<T> value, Func<T, T> func)
        {
            if (value.GetType() == typeof(T[]))
            {
                for (int i = 0; i < (value as T[]).Length; i++)
                {
                    value[i] = func(value[i]);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    value[i] = func(value[i]);
                }
            }
        }

        internal static void ForEach<T>(this IList<T> value, Action<T> action)
        {
            if (value.GetType() == typeof(T[]))
            {
                for (int i = 0; i < (value as T[]).Length; i++)
                {
                    action(value[i]);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    action(value[i]);
                }
            }
        }

        internal static void For<T>(this IList<T> value, Func<int, T> func)
        {
            if (value.GetType() == typeof(T[]))
            {
                for (int i = 0; i < (value as T[]).Length; i++)
                {
                    value[i] = func(i);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    value[i] = func(i);
                }
            }
        }

        internal static void For<T>(this IList<T> value, Action<int> action)
        {
            if (value.GetType() == typeof(T[]))
            {
                for (int i = 0; i < (value as T[]).Length; i++)
                {
                    action(i);
                }
            }
            else
            {
                for (int i = 0; i < value.Count; i++)
                {
                    action(i);
                }
            }
        }
    }
}
