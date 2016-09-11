using System;
using System.Runtime.InteropServices;

namespace Mentula.GuiItems.Design
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}