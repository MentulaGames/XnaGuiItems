#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core
{
#if MONO
    using Mono.Microsoft.Xna.Framework.Input;
#else
    using Xna.Microsoft.Xna.Framework.Input;
#endif
    using System;

    /// <summary>
    /// Represents a class for classes that contain value changing event data.
    /// </summary>
    /// <typeparam name="TVal"> The specified value type to change. </typeparam>
    public sealed class ValueChangedEventArgs<TVal> : EventArgs
    {
        /// <summary>
        /// Provides a value to use with events that do not have event data.
        /// </summary>
        new public static readonly ValueChangedEventArgs<TVal> Empty = new ValueChangedEventArgs<TVal>();

        /// <summary>
        /// The old value.
        /// </summary>
        public readonly TVal OldValue;
        /// <summary>
        /// The new value.
        /// </summary>
        public readonly TVal NewValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueChangedEventArgs{TVal}"/> class with specified parameters.
        /// </summary>
        /// <param name="oldVal"> The old value of the field. </param>
        /// <param name="newVal"> The new value of the field. </param>
        public ValueChangedEventArgs(TVal oldVal, TVal newVal)
        {
            OldValue = oldVal;
            NewValue = newVal;
        }

        private ValueChangedEventArgs()
        {
            OldValue = default(TVal);
            NewValue = default(TVal);
        }
    }

    /// <summary>
    /// Represents a class for classes that contain mouse related event data.
    /// </summary>
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

    /// <summary>
    /// Represents a class for classes that contain indexed click related event data.
    /// </summary>
    public sealed class IndexedClickEventArgs : EventArgs
    {
        /// <summary>
        /// Provides a value to use with events that do not have event data.
        /// </summary>
        new public static IndexedClickEventArgs Empty = new IndexedClickEventArgs();

        /// <summary>
        /// The index of the value clicked.
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedClickEventArgs"/> class with a specified index.
        /// </summary>
        /// <param name="index"> The index of the value clicked. </param>
        public IndexedClickEventArgs(int index)
        {
            Index = index;
        }

        private IndexedClickEventArgs()
        {
            Index = -1;
        }
    }
}