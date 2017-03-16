namespace Mentula.GuiItems.Core.EventHandlers
{
    using System;

    /// <summary>
    /// Represents a class for classes that contain indexed click related event data.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
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