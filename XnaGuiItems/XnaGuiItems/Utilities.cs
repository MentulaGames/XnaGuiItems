using Mentula.GuiItems.Core;
using Mentula.GuiItems.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Mentula.GuiItems
{
    /// <summary>
    /// Contains utilitie functions for graphical use.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    public static class Utilities
    {
        /// <summary>
        /// Changes the borderstyle of the game window.
        /// </summary>
        /// <param name="newType"> 
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Code</term>
        ///             <description>Meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>0</term>
        ///             <description>None</description>
        ///         </item>
        ///         <item>
        ///             <term>1</term>
        ///             <description>FixedSingle</description>
        ///         </item>
        ///          <item>
        ///             <term>2</term>
        ///             <description>Fixed3D</description>
        ///         </item>
        ///          <item>
        ///             <term>3</term>
        ///             <description>FixedDialog</description>
        ///         </item>
        ///          <item>
        ///             <term>4</term>
        ///             <description>Sizable</description>
        ///         </item>
        ///          <item>
        ///             <term>5</term>
        ///             <description>FixedToolWindow</description>
        ///         </item>
        ///          <item>
        ///             <term>6</term>
        ///             <description>SizableWindow</description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="game"> The associated game window. </param>
        /// <remarks>
        /// A byte is used to represent the <see cref="FormBorderStyle"/> enumeraton 
        /// as not to require a refrence to the <see cref="System.Windows.Forms"/> namespace.
        /// </remarks>
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

        /*
            Safely invokes the delegate (is not null) and throws an InvokerException if the delegate throws an exception
        */
        internal static void Invoke(Delegate function, params object[] args)
        {
            if (function != null)
            {
                try { function.DynamicInvoke(args); }
                catch (TargetInvocationException e) { throw new InvokeException(e); }
            }
        }

        /*
            Updates the GuiItem with its desired update method.
        */
        internal static void Update_S(this GuiItem control, MouseState mState, KeyboardState kState, float delta)
        {
            IDeltaUpdate dUpd;
            IDeltaKeyboardUpdate dkUpd;

            if ((dUpd = control as IDeltaUpdate) != null) dUpd.Update(mState, delta);
            else if ((dkUpd = control as IDeltaKeyboardUpdate) != null) dkUpd.Update(mState, kState, delta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Thread CreateBackgroundThread(ThreadStart func)
        {
            return new Thread(func) { IsBackground = true };
        }
    }
}