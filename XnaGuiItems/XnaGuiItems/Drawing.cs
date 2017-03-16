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
    using Core.Structs;
    using System;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class Drawing
    {
        public static Texture2D FromColor(Color color, Size size, GraphicsDevice device)
        {
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
    }
}