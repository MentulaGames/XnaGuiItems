using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Windows.Forms;

namespace Mentula.GuiItems
{
    public class Utilities
    {
        /// <summary>
        /// 0 = None,
        /// 1 = FixedSingle,
        /// 2 = Fixed3D,
        /// 3 = FixedDialog,
        /// 4 = Sizable,
        /// 5 = FixedToolWindow,
        /// 6 = SizableWindow.
        /// </summary>
        /// <param name="newType"> The new border type (1 to 6) </param>
        [DebuggerHidden]    // Secret code, nobody talks about it. SPOOKY
        public static void ChangeWindowBorder(Game game, byte newType)
        {
            if (newType > 6) return;

            FormBorderStyle n = (FormBorderStyle)newType;
            Control window = Control.FromHandle(game.Window.Handle);
            window.FindForm().FormBorderStyle = n;
        }
    }
}