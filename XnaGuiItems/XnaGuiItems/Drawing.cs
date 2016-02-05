using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mentula.GuiItems
{
     internal static class Drawing
     {
          internal static Texture2D FromColor(Color color, int width, int height, GraphicsDevice device)
          {
               Texture2D texture = new Texture2D(device, width, height);

               Color[] data = new Color[width * height];
               data.ForEach(i => i = color);
               texture.SetData<Color>(data);

               return texture;
          }

          internal static Texture2D FromColor(Color color, int width, int height, Rectangle destinationRectangle, GraphicsDevice device)
          {
               Texture2D texture = new Texture2D(device, width, height);
               Color[] data = new Color[width * height];

               data.For(delegate(int i)
               {
                    Vector2 pos = new Vector2(i % width, (i - (i % width)) / width);
                    if (destinationRectangle.Contains(pos.ToPoint())) return color;
                    else return Color.Transparent;
               });

               texture.SetData<Color>(data);
               return texture;
          }

          internal static Texture2D FromText(string text, SpriteFont font, Color color, int width, int height, bool multiLine, GraphicsDevice device)
          {
               string[] drawAbleText = multiLine ? text.Split(new string[1] { "/n" }, StringSplitOptions.None) : new string[1] { text };
               RenderTarget2D target = new RenderTarget2D(device, width, height);
               SpriteBatch sb = new SpriteBatch(device);

               device.SetRenderTarget(target);
               device.Clear(Color.Transparent);

               sb.Begin();
               for (int i = 0; i < drawAbleText.Length; i++)
               {
                    sb.DrawString(font, drawAbleText[i], new Vector2(1, 1 + i * font.MeasureString("a").Y), color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
               }
               sb.End();

               device.SetRenderTarget(null);

               Texture2D texture = new Texture2D(device, width, height);
               Color[] colorData = target.GetColorData();
               texture.SetData<Color>(colorData);

               target.Dispose();

               return texture;
          }
     }
}
