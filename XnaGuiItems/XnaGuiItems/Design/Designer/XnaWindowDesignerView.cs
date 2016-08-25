using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mentula.GuiItems.Design.Designer
{
    internal sealed class XnaWindowDesignerView : XnaGuiControl
    {
        public XnaWindowDesignerView(GraphicsDevice device)
            : base(new ImagedGuiItem(device)
            {
                Name = "XnaWindow",
                BackColor = Color.Black,
                Bounds = device.Viewport.Bounds
            })
        {
            Refresh();
        }
    }
}