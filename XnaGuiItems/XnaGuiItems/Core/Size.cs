﻿using Microsoft.Xna.Framework;
using System;
using static Mentula.GuiItems.Utilities;

namespace Mentula.GuiItems.Core
{
    /// <summary>
    /// Stores an ordered pair of integers, wich specify a Height and Width.
    /// </summary>
    /// <remarks>
    /// This object is a copy of the <see cref="System.Drawing.Size"/> 
    /// and is only present in this library so a refrence to <see cref="System.Drawing"/> is not required.
    /// </remarks>
    public struct Size : IEquatable<Size>
    {
        /// <summary>
        /// Gets or sets the vertical component of this Size structure.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Gets or sets the horizontal component of this Size structure.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Tests whether this Size structure has width and height of 0.
        /// </summary>
        public bool IsEmpty { get { return Height == 0 && Width == 0; } }

        /// <summary>
        /// Gets a Size structure that has a <see cref="Height"/> and <see cref="Width"/> value of 0.
        /// </summary>
        public static readonly Size Empty = new Size();

        /// <summary>
        /// Adds the width and height of one Size structure to the width and height of another Size structure.
        /// </summary>
        /// <param name="sz1"> The first Size to add. </param>
        /// <param name="sz2"> The second Size to add. </param>
        /// <returns> A Size structure that is the result of the addition operation. </returns>
        public static Size operator +(Size sz1, Size sz2) { return Add(sz1, sz2); }
        /// <summary>
        /// Subtracts the width and height of one Size structure from the width and height of another Size structure.
        /// </summary>
        /// <param name="sz1"> The first Size to subtract. </param>
        /// <param name="sz2"> The second Size to subtract. </param>
        /// <returns> A Size structure that is the result of the subtraction operation. </returns>
        public static Size operator -(Size sz1, Size sz2) { return Subtract(sz1, sz2); }
        /// <summary>
        /// Tests whether two <see cref="Size"/> structures are equal.
        /// </summary>
        /// <param name="sz1"> The Size structure on the left side of the equality operator. </param>
        /// <param name="sz2"> The Size structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if sz1 and sz2 have equal width and height; otherwise <see langword="false"/>. </returns>
        public static bool operator ==(Size sz1, Size sz2) { return Equals(sz1, sz2); }
        /// <summary>
        /// Tests whether two <see cref="Size"/> structures are different.
        /// </summary>
        /// <param name="sz1"> The Size structure on the left side of the inequality operator. </param>
        /// <param name="sz2"> The Size structure on the right side of the inequality operator. </param>
        /// <returns> <see langword="true"/> if sz1 and sz2 differ either in width or height; <see langword="false"/> if sz1 and sz2 are equal. </returns>
        public static bool operator !=(Size sz1, Size sz2) { return !Equals(sz1, sz2); }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> structure fomr the specified dimensions.
        /// </summary>
        /// <param name="width"> The width component of the new <see cref="Size"/>. </param>
        /// <param name="height"> The height component of the new <see cref="Size"/>. </param>
        public Size(int width, int height)
        {
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> structure from the specifie <see cref="Vector2"/> structure.
        /// </summary>
        /// <param name="vect"> The <see cref="Vector2"/> structure from which to initialize this <see cref="Size"/> structure. </param>
        public Size(Vector2 vect)
        {
            Height = (int)vect.Y;
            Width = (int)vect.X;
        }

        /// <summary>
        /// Adds the width and height of one Size structure to the width and height of another Size structure.
        /// </summary>
        /// <param name="size1"> The first Size to add. </param>
        /// <param name="size2"> The second Size to add. </param>
        /// <returns> A Size structure that is the result of the addition operation. </returns>
        public static Size Add(Size size1, Size size2)
        {
            return new Size(size1.Width + size2.Width, size1.Height + size2.Height);
        }

        /// <summary>
        /// Subtracts the width and height of one Size structure from the width and height of another Size structure.
        /// </summary>
        /// <param name="size1"> The first Size to subtract. </param>
        /// <param name="size2"> The second Size to subtract. </param>
        /// <returns> A Size structure that is the result of the subtraction operation. </returns>
        public static Size Subtract(Size size1, Size size2)
        {
            return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
        }

        /// <summary>
        /// Tests to see whether the specified object is a Size structure with the same dimentions as this Size structure.
        /// </summary>
        /// <param name="obj"> The Object to test. </param>
        /// <returns> <see langword="true"/> if obj is a Size and has the same width and height as this Size; otherwise, <see langword="false"/>. </returns>
        public override bool Equals(object obj)
        {
            if (obj is Size) return Equals((Size)obj);
            return false;
        }

        /// <summary>
        /// Tests to see whether the specified Size has the same dimentions as this Size structure.
        /// </summary>
        /// <param name="size"> The Size to test. </param>
        /// <returns> <see langword="true"/> if size has the same width and height as this Size; otherwise, <see langword="false"/>. </returns>
        public bool Equals(Size size)
        {
            return size.Width == Width && size.Height == Height;
        }

        /// <summary>
        /// Returns a hash code for this Size structure.
        /// </summary>
        /// <returns> An integer value that specifies a hash value for this Size structure. </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = HASH_BASE;
                hash += ComputeHash(hash, Width);
                hash += ComputeHash(hash, Height);
                return hash;
            }
        }

        /// <summary>
        /// Creates a human-readable string that represents this Size structure.
        /// </summary>
        /// <returns> A string that represents this <see cref="Size"/>. </returns>
        public override string ToString()
        {
            return $"Width:{Width} Height:{Height}";
        }
    }
}