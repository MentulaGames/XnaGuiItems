#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core.EventHandlers
{
#if MONO
    using Mono::Microsoft.Xna.Framework.Input;
#else
    using Xna::Microsoft.Xna.Framework.Input;
#endif
    using System;

    /// <summary>
    /// Represents a class for classes that contain mouse related event data.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class MouseEventArgs : EventArgs
    {
        /// <summary>
        /// Provides a value to use with events that do not have event data.
        /// </summary>
        new public static readonly MouseEventArgs Empty = new MouseEventArgs();

        /// <summary>
        /// The current state of the mouse that was used for invoking the event.
        /// </summary>
        public readonly MouseState State;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class with a specified <see cref="MouseState"/>.
        /// </summary>
        /// <param name="state"> The current state of the <see cref="Mouse"/>. </param>
        public MouseEventArgs(MouseState state)
        {
            State = state;
        }

        private MouseEventArgs()
        {
            State = new MouseState();
        }
    }
}