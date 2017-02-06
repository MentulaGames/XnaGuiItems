namespace Mentula.GuiItems
{
    using Core;
    using Core.Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Contains utilitie functions for graphical use.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    public static class Utilities
    {
        /* When initializing the guiItems don't need to be refreshed 3 times so it is internaly suppressed. */
        internal static bool suppressRefresh;

        internal const int HASH_BASE = unchecked((int)2166136261);
        private const int HASH_MODIFIER = 16777619;

        internal const string 
            CAT_DESIGN = "Microsoft.Design",
            CAT_USAGE = "Microsoft.Usage",
            CHECKID_EVENT = "CA1009:DeclareEventHandlersCorrectly",
            CHECKID_CALL = "CA2214:DoNotCallOverridableMethodsInConstructors",
            JUST_VIRT_FINE = "Missing calls causes no harm in this instance.",
            JUST = "Using strong-typed GuiItemEventHandler event handler pattern.",
            JUST_INDEX = "Using strong-typed IndexedClickEventHandler<GuiItem, IndexedClickEventArgs> event handler pattern.",
            JUST_MOUSE = "Using strong-typed MouseEventHandler<GuiItem, MouseEventArgs> event handler pattern.",
            JUST_VALUE = "Using strong-typed ValueChangedEventHandler<GuiItem, ValueChangedEventArgs> event handler pattern.";

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

        /* Computes the hash value of a object's field. */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int ComputeHash<T>(int hash, T obj)
        {
            return hash * HASH_MODIFIER ^ obj.GetHashCode();
        }

        /*
            Safely invokes the delegate (is not null) and throws an InvokerException if the delegate throws an exception
        */
        internal static void Invoke(Delegate function, object sender, EventArgs args)
        {
            if (function != null)
            {
                try { function.DynamicInvoke(new object[2] { sender, args }); }
                catch (TargetInvocationException e) { throw new InvokeException(e); }
            }
        }

        internal static void Invoke<T>(ValueChangedEventHandler<T> function, GuiItem sender, ValueChangedEventArgs<T> args)
        {
            if (function != null)
            {
                try { function.Invoke(sender, args); }
                catch (TargetInvocationException e) { throw new InvokeException(e); }
            }
        }

        internal static void Invoke(Core.MouseEventHandler function, GuiItem sender, Core.MouseEventArgs args)
        {
            if (function != null)
            {
                try { function.Invoke(sender, args); }
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
            else control.Update(mState);
        }

        internal static void AddSet<Tkey, TVal>(this Dictionary<Tkey, TVal> dict, Tkey key, TVal val)
        {
            if (dict.ContainsKey(key)) dict[key] = val;
            else dict.Add(key, val);
        }

        internal static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Thread CreateBackgroundThread(ThreadStart func)
        {
            return new Thread(func) { IsBackground = true };
        }
    }
}