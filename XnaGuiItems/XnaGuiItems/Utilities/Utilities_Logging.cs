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
        internal static void LogInvokeCall(string sender, string func) => LogXna(sender, $"called Invoke for {func}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogInit(string sender, string msg) => LogSpecType(sender, msg, LogMsgType.Init);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogGphx(string sender, string msg) => LogSpecType(sender, msg, LogMsgType.Gphx);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogXna(string sender, string msg) => LogMsg("XnaGuiItems", sender, type.ToString(), msg);

        internal static void LogSpecType(string sender, string msg, LogMsgType t)
        {
            type = t;
            LogXna(sender, msg);
            type = LogMsgType.Call;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogMsg(string lib, string sender, string type, string msg)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd H:mm:ss}][{lib}][{type}] {sender}: {msg}.");
        }
    }
#endif
}