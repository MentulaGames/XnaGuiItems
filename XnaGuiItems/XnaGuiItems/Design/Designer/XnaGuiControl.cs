using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mentula.GuiItems.Design.Designer
{
    [ToolboxItem(true)]
    internal class XnaGuiControl : Control
    {
        private ImagedGuiItem item;

        public XnaGuiControl()
        { }

        public XnaGuiControl(ImagedGuiItem item)
        {
            this.item = item;
            BackColor = Color.Black;
        }

        public override void Refresh()
        {
            item.Refresh();
            base.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (item != null) e.Graphics.DrawImage(item.Image, new PointF(item.Position.X, item.Position.Y));
            else e.Graphics.DrawEllipse(Pens.Yellow, 10, 10, 100, 100);
        }
    }
}