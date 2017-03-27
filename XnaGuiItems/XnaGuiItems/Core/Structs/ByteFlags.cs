namespace Mentula.GuiItems.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Contains 8 <see cref="bool"/> values stored as 1 <see cref="byte"/>.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{ToString()}")]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Serializable]
    public struct ByteFlags : IEquatable<ByteFlags>
    {
        private byte underlying;

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="ByteFlags"/> are equal.
        /// </summary>
        /// <param name="left"> The current instance of <see cref="ByteFlags"/>. </param>
        /// <param name="right"> The <see cref="ByteFlags"/> to compare with the current instance. </param>
        /// <returns> <see langword="true"/> if obj and this instance are the same type and represent the same value; otherwise, <see langword="false"/>. </returns>
        public static bool operator ==(ByteFlags left, ByteFlags right) { return left.Equals(right); }
        /// <summary>
        /// Indicates whether this instance and a specified <see cref="ByteFlags"/> are different.
        /// </summary>
        /// <param name="left"> The current instance of <see cref="ByteFlags"/>. </param>
        /// <param name="right"> The <see cref="ByteFlags"/> to compare with the current instance. </param>
        /// <returns> <see langword="false"/> if obj and this instance are the same type and represent the same value; otherwise, <see langword="true"/>. </returns>
        public static bool operator !=(ByteFlags left, ByteFlags right) { return !left.Equals(right); }

        /// <summary>
        /// Gets or sets the value at index i.
        /// </summary>
        /// <param name="i"> The specified index. </param>
        /// <returns> The value of the bool. </returns>
        /// <exception cref="IndexOutOfRangeException"> This exception is thrown if the index i if smaller than 0 or greater than 7. </exception>
        public bool this[int i]
        {
            get
            {
                CheckRange(i);
                return (underlying & GetMask(i)) != 0;
            }

            set
            {
                CheckRange(i);
                if (value) underlying |= GetMask(i);
                else underlying &= (byte)(~GetMask(i));
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="ByteFlags"/> are equal.
        /// </summary>
        /// <param name="other"> The <see cref="ByteFlags"/> to compare with the current instance. </param>
        /// <returns> <see langword="true"/> if obj and this instance are the same type and represent the same value; otherwise, <see langword="false"/>. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ByteFlags other) => other.underlying == underlying;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj"> The object to compare with the current instance. </param>
        /// <returns>
        /// <see langword="true"/> if obj and this instance are the same type and represent the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(ByteFlags) ? Equals((ByteFlags)obj) : false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns> A 32-bit signed integer that is the hash code for this instance. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => underlying;

        /// <summary>
        /// Returns a visual representation of the <see cref="ByteFlags"/>.
        /// </summary>
        /// <returns> A string version of the underlying byte. </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 7; i >= 0; --i)
            {
                sb.Append(this[i] ? '1' : '0');
            }

            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private byte GetMask(int i) => (byte)(i > 0 ? 1 << i : 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckRange(int i)
        {
            if (i > 7 || i < 0) throw new IndexOutOfRangeException();
        }
    }
}