namespace Mentula.GuiItems
{
#if DEBUG
    using Core;
    using System;
    using System.Runtime.CompilerServices;

    public static partial class Utilities
    {
        internal static LogMsgType type;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogInvokeCall(string sender, string func)
        {
            LogBase(sender, $"called Invoke for {func}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogInit(string sender, string msg)
        {
            type = LogMsgType.Init;
            LogBase(sender, msg);
            type = LogMsgType.Call;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogGphx(string sender, string msg)
        {
            type = LogMsgType.Gphx;
            LogBase(sender, msg);
            type = LogMsgType.Gphx;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogBase(string sender, string msg)
        {
            LogMsg("XnaGuiItems", sender, type.ToString(), msg);
        }

        public static void LogMsg(string lib, string sender, string type, string msg)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd H:mm:ss}][{lib}][{type}] {sender}: {msg}.");
        }
    }
#endif
}