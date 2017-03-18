namespace Mentula.GuiItems
{
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static partial class Utilities
    {
        /// <summary> Runs a specified <see cref="ThreadStart"/> on a background STA thread. </summary>
        /// <param name="function"> The specified function to run. </param>
        public static void RunInSTAThread(ThreadStart function)
        {
            Thread t = CreateBackgroundThread(function);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary> Runs a specified <see cref="ThreadStart"/> on a background thread. </summary>
        /// <param name="function"> The specified function to run. </param>
        public static void RunInBackground(ThreadStart function)
        {
            CreateBackgroundThread(function).Start();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Thread CreateBackgroundThread(ThreadStart func)
        {
            return new Thread(func) { IsBackground = true };
        }
    }
}