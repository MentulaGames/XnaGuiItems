﻿namespace Mentula.GuiItems.Core
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using static Utilities;

    /// <summary>
    /// Stores pairs of <see cref="string"/> and <see cref="Microsoft.Xna.Framework.Color"/>.
    /// </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{Text}")]
    public struct Pair : IEquatable<Pair>
    {
        /// <summary>
        /// The text of the pair label.
        /// </summary>
        public readonly string Text;
        /// <summary>
        /// The color of the pair label.
        /// </summary>
        public readonly Color? Color;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair"/> struct with a specified text.
        /// </summary>
        /// <param name="txt"> The specified text. </param>
        public Pair(string txt)
        {
            Text = txt;
            Color = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair"/> struct with 
        /// a specified text and a specified color.
        /// </summary>
        /// <param name="txt"> The specified text. </param>
        /// <param name="clr"> The specified color. </param>
        public Pair(string txt, Color clr)
        {
            Text = txt;
            Color = clr;
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="Pair"/> are equal.
        /// </summary>
        /// <param name="other"> The <see cref="Pair"/> to compare with the current instance. </param>
        /// <returns> <see langword="true"/> if obj and this instance are the same type and represent the same value; otherwise, <see langword="false"/>. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Pair other) => other.Text == Text && other.Color == Color;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj"> The object to compare with the current instance. </param>
        /// <returns>
        /// <see langword="true"/> if obj and this instance are the same type and represent the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Pair) ? Equals((Pair)obj) : false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns> A 32-bit signed integer that is the hash code for this instance. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = HASH_BASE;
                hash += ComputeHash(hash, Text);
                hash += ComputeHash(hash, Color);
                return hash;
            }
        }

        /// <summary>
        /// Returns <see cref="Text"/>.
        /// </summary>
        /// <returns> <see cref="Text"/>. </returns>
        public override string ToString()
        {
            return Text;
        }
    }
}