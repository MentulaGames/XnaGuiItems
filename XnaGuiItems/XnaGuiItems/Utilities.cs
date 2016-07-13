using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Mentula.GuiItems
{
    /// <summary>
    /// Contains utilitie functions for graphical use.
    /// </summary>
    [DebuggerStepThrough]
    public static class Utilities
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
        /// <param name="game"> The associated game window. </param>
        [DebuggerHidden]    // Secret code, nobody talks about it. SPOOKY
        public static void ChangeWindowBorder(Game game, byte newType)
        {
            if (newType > 6) return;

            FormBorderStyle n = (FormBorderStyle)newType;
            Control window = Control.FromHandle(game.Window.Handle);
            window.FindForm().FormBorderStyle = n;
        }

        /// <summary> Runs a specified <see cref="ThreadStart"/> on a background STA thread. </summary>
        /// <param name="function"> The specified function to run. </param>
        public static void RunInSTAThread(ThreadStart function)
        {
            Thread t = new Thread(function) { IsBackground = true };
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary> Runs a specified <see cref="ThreadStart"/> on a background thread. </summary>
        /// <param name="function"> The specified function to run. </param>
        public static void RunInBackground(ThreadStart function)
        {
            Thread t = new Thread(function) { IsBackground = true };
            t.Start();
        }

        internal static int Clamp(this int value, int max, int min)
        {
            return value > max ? max : (value < min ? min : value);
        }

        internal static void Invoke(Delegate function, params object[] args)
        {
            if (function != null) function.DynamicInvoke(args);
        }
    }
}