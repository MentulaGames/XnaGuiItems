using Mentula.GuiItems.Design.Designer;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Mentula.GuiItems.Design
{
    [Designer(typeof(GuiItemDesigner), typeof(IRootDesigner))]
    internal class BaseControl : XnaGuiControl
    {
        public BaseControl()
        {

        }
    }
}