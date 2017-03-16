namespace Mentula.GuiItems.Core.EventHandlers
{
    using System;

    /// <summary>
    /// Represents a class for classes that contain value changing event data.
    /// </summary>
    /// <typeparam name="TVal"> The specified value type to change. </typeparam>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class ValueChangedEventArgs<TVal> : EventArgs
    {
        /// <summary>
        /// Provides a value to use with events that do not have event data.
        /// </summary>
        new public static readonly ValueChangedEventArgs<TVal> Empty = new ValueChangedEventArgs<TVal>();

        internal bool NoChange
        {
            get
            {
                if (OldValue != null) return OldValue.Equals(NewValue);
                else return NewValue == null;
            }
        }

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
}