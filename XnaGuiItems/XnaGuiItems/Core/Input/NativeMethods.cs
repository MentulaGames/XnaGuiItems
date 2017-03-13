namespace Mentula.GuiItems.Core.Input
{
    using System.Runtime.InteropServices;

#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    internal static class NativeMethods
    {
        public static bool CapsLockState() => (GetKeyState(0x14) & 0xFFFF) != 0;
        public static bool NumLockState() => (GetKeyState(0x190) & 0xFFFF) != 0;
        public static bool ScrollLockState() => (GetKeyState(0x91) & 0xFFFF) != 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetKeyState(int keyCode);
    }
}
