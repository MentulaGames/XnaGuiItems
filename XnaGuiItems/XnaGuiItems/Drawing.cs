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
#else
    using Xna::Microsoft.Xna.Framework;
    using Xna::Microsoft.Xna.Framework.Graphics;
#endif
    using Core;
    using Core.Structs;
    using System;
    using static Utilities;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class Drawing
    {
        public static Texture2D FromColor(Color color, Size size, GraphicsDevice device)
        {
#if DEBUG
            LogGphx("Renderer", "created texture from color");
#endif

            Texture2D result = new Texture2D(device, size.Width, size.Height);
            Color[] data = new Color[size.Width * size.Height];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            result.SetData(data);
            return result;
        }

        public static Texture2D FromColor(Color color, Size size, Rect destinationRectangle, GraphicsDevice device)
        {
#if DEBUG
            LogGphx("Renderer", "created texture from color");
#endif

            Texture2D result = new Texture2D(device, size.Width, size.Height);
            Color[] data = new Color[size.Width * size.Height];

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    data[y * size.Width + x] = destinationRectangle.Contains(x, y) ? color : Color.Transparent;
                }
            }

            result.SetData(data);
            return result;
        }

        public static Texture2D FromMultiColor(Color color0, Color color1, Rectangle destination0, Rectangle destination1, GraphicsDevice device)
        {
#if DEBUG
            LogGphx("Renderer", "created texture from multiple colors");
#endif
            
            Texture2D result = new Texture2D(device, Math.Max(destination0.Width, destination1.Width), Math.Max(destination0.Height, destination1.Height));
            Color[] data = new Color[result.Width * result.Height];

            for (int y = destination0.Y; y < destination0.Height; y++)
            {
                for (int x = destination0.X; x < destination0.Width; x++)
                {
                    data[y * result.Width + x] = color0;
                }
            }

            for (int y = destination1.Y; y < destination1.Height; y++)
            {
                for (int x = destination1.X; x < destination1.Width; x++)
                {
                    data[y * result.Width + x] = color1;
                }
            }

            result.SetData(data);
            return result;
        }

        public static Texture2D FromText(string text, SpriteFont font, Color color, Size size, bool multiLine, int lineStart, SpriteBatch sb)
        {
#if DEBUG
            LogGphx("Renderer", "created texture from string");
#endif

            Texture2D result = new Texture2D(sb.GraphicsDevice, size.Width, size.Height);

            using (RenderTarget2D target = new RenderTarget2D(sb.GraphicsDevice, size.Width, size.Height))
            {
                sb.GraphicsDevice.SetRenderTarget(target);
                sb.GraphicsDevice.Clear(Color.Transparent);

                sb.Begin();
                if (multiLine)
                {
                    float fH = font.GetHeight();
                    string[] lines = text.Split('\n');

                    for (int i = lineStart; i < lines.Length; i++)
                    {
                        float y = 1 + (i - lineStart) * fH;
                        sb.DrawString(font, lines[i], new Vector2(1, y), color);
                    }
                }
                else sb.DrawString(font, text, Vector2.One, color);
                sb.End();

                sb.GraphicsDevice.SetRenderTarget(null);
                result.SetData(target.GetColorData());
            }

            return result;
        }

        public static Texture2D FromLabels(Pair[] labels, SpriteFont font, Size size, SpriteBatch sb)
        {
#if DEBUG
            LogGphx("Renderer", "created texture from string|color labels");
#endif

            Texture2D result = new Texture2D(sb.GraphicsDevice, size.Width, size.Height);

            using (RenderTarget2D target = new RenderTarget2D(sb.GraphicsDevice, size.Width, size.Height))
            {
                sb.GraphicsDevice.SetRenderTarget(target);
                sb.GraphicsDevice.Clear(Color.Transparent);

                string[] labelText = Pair.GetKeys(labels);

                sb.Begin();
                for (int i = 0; i < labels.Length; i++)
                {
                    string prevString = string.Join(" ", labelText, 0, i);
                    if (i > 0) prevString += ' ';
                    float x = 1 + font.MeasureString(prevString).X;

                    sb.DrawString(font, labels[i].Text, new Vector2(x, 1), labels[i].Color.Value);
                }
                sb.End();

                sb.GraphicsDevice.SetRenderTarget(null);
                result.SetData(target.GetColorData());
            }

            return result;
        }

        public static Texture2D ApplyBorderLabel(this Texture2D texture, BorderStyle style)
        {
#if DEBUG
            LogGphx("Renderer", "applied borders to label");
#endif

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
#if DEBUG
            LogGphx("Renderer", "applied borders to button");
#endif

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
#if DEBUG
            LogGphx("Renderer", $"clipped texture");
#endif

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
#if DEBUG
            LogGphx("Renderer", $"renderer texture onto parent texture");
#endif

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
#if DEBUG
            LogGphx("Renderer", $"stretched texture");
#endif

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
    }
}