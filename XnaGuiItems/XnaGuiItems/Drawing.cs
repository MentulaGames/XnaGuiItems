using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mentula.GuiItems
{
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class Drawing
    {
        internal static Texture2D FromColor(Color color, int width, int height, GraphicsDevice device)
        {
            Texture2D texture = new Texture2D(device, width, height);

            Color[] data = new Color[width * height];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            texture.SetData(data);

            return texture;
        }

        internal static Texture2D FromColor(Color color, int width, int height, Rectangle destinationRectangle, GraphicsDevice device)
        {
            Texture2D texture = new Texture2D(device, width, height);
            Color[] data = new Color[width * height];

            for (int i = 0; i < data.Length; i++)
            {
                Vector2 pos = new Vector2(i % width, (i - (i % width)) / width);
                if (destinationRectangle.Contains(pos.ToPoint())) data[i] = color;
                else data[i] = Color.Transparent;
            }

            texture.SetData(data);
            return texture;
        }

        internal static Texture2D FromText(string text, SpriteFont font, Color color, int width, int height, bool multiLine, int lineStart, GraphicsDevice device)
        {
            string[] drawAbleText = multiLine ? text.Split(new string[1] { "\n" }, StringSplitOptions.None) : new string[1] { text };
            RenderTarget2D target = new RenderTarget2D(device, width, height);
            SpriteBatch sb = new SpriteBatch(device);

            device.SetRenderTarget(target);
            device.Clear(Color.Transparent);

            sb.Begin();
            for (int i = lineStart; i < drawAbleText.Length; i++)
            {
                float y = 1 + (i - lineStart) * font.MeasureString("a").Y;
                sb.DrawString(font, drawAbleText[i], new Vector2(1, y), color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            }
            sb.End();

            device.SetRenderTarget(null);

            Texture2D texture = new Texture2D(device, width, height);
            Color[] colorData = target.GetColorData();
            texture.SetData(colorData);

            target.Dispose();

            return texture;
        }

        internal static Texture2D FromLabels(KeyValuePair<string, Color>[] labels, SpriteFont font, int width, int height, GraphicsDevice device)
        {
            RenderTarget2D target = new RenderTarget2D(device, width, height);
            SpriteBatch sb = new SpriteBatch(device);

            device.SetRenderTarget(target);
            device.Clear(Color.Transparent);

            sb.Begin();
            for (int i = 0; i < labels.Length; i++)
            {
                string prevString = string.Join(" ", labels.Select(l => l.Key).ToArray(), 0, i);
                if (i > 0) prevString += " ";
                float x = 1 + font.MeasureString(prevString).X;

                sb.DrawString(font, labels[i].Key, new Vector2(x, 1), labels[i].Value, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            }
            sb.End();

            device.SetRenderTarget(null);

            Texture2D texture = new Texture2D(device, width, height);
            Color[] colorData = target.GetColorData();
            texture.SetData(colorData);

            target.Dispose();

            return texture;
        }
    }
}