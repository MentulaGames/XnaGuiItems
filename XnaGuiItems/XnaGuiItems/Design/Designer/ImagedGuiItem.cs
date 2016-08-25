using Mentula.GuiItems.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bitmap = System.Drawing.Bitmap;
using DColor = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace Mentula.GuiItems.Design.Designer
{
    internal class ImagedGuiItem : GuiItem
    {
        public Image Image
        {
            get
            {
                if (image == null) Refresh();
                return image;
            }
        }

        private Image image;

        public ImagedGuiItem(GraphicsDevice device)
            : base(device)
        { }

        public override void Refresh()
        {
            base.Refresh();

            using (RenderTarget2D target = new RenderTarget2D(device, Bounds.Width, Bounds.Height))
            {
                SpriteBatch sb = new SpriteBatch(device);

                device.SetRenderTarget(target);
                device.Clear(Color.Transparent);

                sb.Begin();
                Draw(sb);
                sb.End();

                device.SetRenderTarget(null);
                image = ToDColorBmp(target.GetColorData(), Bounds.Width, Bounds.Height);
            }
        }

        private Bitmap ToDColorBmp(Color[] data, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = ((y * width) + x);
                    Color oC = data[i];
                    DColor nC = DColor.FromArgb(oC.A, oC.R, oC.G, oC.B);

                    bmp.SetPixel(x, y, nC);
                }
            }

            return bmp;
        }
    }
}