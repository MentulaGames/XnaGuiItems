using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace Mentula.GuiItems.Design.Designer
{
    //[ToolboxItemFilter("Mentula.GuiItems", ToolboxItemFilterType.Require)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    internal class GuiItemDesigner : ParentControlDesigner, IRootDesigner, IToolboxUser
    {
        public ViewTechnology[] SupportedTechnologies
        {
            get
            {
                return new ViewTechnology[] { ViewTechnology.Default };
            }
        }

        private XnaWindowDesignerView view;
        private GraphicsDevice device;

        public GuiItemDesigner()
        {
            try
            {
                PresentationParameters presParams = new PresentationParameters
                {
                    DeviceWindowHandle = NativeMethods.GetForegroundWindow(),
                    IsFullScreen = false
                };

                device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, presParams);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not create GraphicsDevice", e);
            }
        }

        public object GetView(ViewTechnology technology)
        {
            if (technology != ViewTechnology.Default)
            {
                throw new ArgumentException("Not a supported view technology", "technology");
            }

            if (view == null) view = new XnaWindowDesignerView(device);
            return view;
        }

        public bool GetToolSupported(ToolboxItem tool)
        {
            return tool.TypeName.StartsWith("Mentula.GuiItems");
        }

        public void ToolPicked(ToolboxItem tool)
        {

        }
    }
}
