#if MONO
extern alias Mono;
#else
extern alias Xna;
#endif

namespace Mentula.GuiItems.Core
{
#if MONO
    using Mono::Microsoft.Xna.Framework;
#else
    using Xna::Microsoft.Xna.Framework;
#endif
    using System;
    using System.Diagnostics;
    using static Mentula.Utilities.Utils;
    using static System.Math;

    /// <summary> Defines a rectangle. </summary>
    /// <remarks>
    /// This object is a copy of the System.Drawing.Rectangle with minor changes.
    /// </remarks>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Rect : IEquatable<Rect>, IEquatable<Rectangle>
    {
        /// <summary> Specifies the x-coordinate of the <see cref="Rect"/>. </summary>
        public float X { get; set; }
        /// <summary> Specifies the y-coordinate of the <see cref="Rect"/>. </summary>
        public float Y { get; set; }
        /// <summary> Specifies the width of the <see cref="Rect"/>. </summary>
        public int Width { get; set; }
        /// <summary> Specifies the height of the <see cref="Rect"/>. </summary>
        public int Height { get; set; }

        /// <summary> Returns the y-coordinate of the bottom of the <see cref="Rect"/>. </summary>
        public float Bottom { get { return Height > 0 ? Y + Height : Y; } }
        /// <summary> Returns a <see cref="Vector2"/> that specifies the center of the <see cref="Rect"/>. </summary>
        public Vector2 Center { get { return Position + (Size / 2).ToVector2(); } }
        /// <summary> Gets a value that indicates whether the <see cref="Rect"/> is empty. </summary>
        public bool IsEmpty { get { return Size.IsEmpty; } }
        /// <summary> Returns the x-coordinate of the left side of the <see cref="Rect"/>. </summary>
        public float Left { get { return Width > 0 ? X : X + Width; } }
        /// <summary> Gets of sets the <see cref="X"/> and <see cref="Y"/> values of the <see cref="Rect"/>. </summary>
        public Vector2 Position { get { return new Vector2(X, Y); } set { X = value.X; Y = value.Y; } }
        /// <summary> Returns the x-coordinate of the right side of the <see cref="Rect"/>. </summary>
        public float Right { get { return Width > 0 ? X + Width : X; } }
        /// <summary> Gets of sets the <see cref="Width"/> and <see cref="Height"/> values of the <see cref="Rect"/>. </summary>
        public Size Size { get { return new Size(Width, Height); } set { Width = value.Width; Height = value.Height; } }
        /// <summary> Returns the x-coordinate of the top of the <see cref="Rect"/>. </summary>
        public float Top { get { return Height > 0 ? Y : Y + Height; } }

        /// <summary>
        /// Gets a <see cref="Rect"/> structure that has its <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> set to 0. 
        /// </summary>
        public static readonly Rect Empty = new Rect();

        /// <summary>
        /// Tests whether two <see cref="Rect"/> structures are equal.
        /// </summary>
        /// <param name="left"> The rect structure on the left side of the equality operator. </param>
        /// <param name="right"> The rect structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if left and right have equal X, Y, width and height; otherwise <see langword="false"/>. </returns>
        public static bool operator ==(Rect left, Rect right) { return left.Equals(right); }
        /// <summary>
        /// Tests whether two <see cref="Rect"/> structures are different.
        /// </summary>
        /// <param name="left"> The rect structure on the left side of the equality operator. </param>
        /// <param name="right"> The rect structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if left and right have equal X, Y, width and height; otherwise <see langword="false"/>. </returns>
        public static bool operator !=(Rect left, Rect right) { return !left.Equals(right); }

        /// <summary>
        /// Tests whether two rectangles are equal.
        /// </summary>
        /// <param name="left"> The rect structure on the left side of the equality operator. </param>
        /// <param name="right"> The rectangle structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if left and right have equal X, Y, width and height; otherwise <see langword="false"/>. </returns>
        public static bool operator ==(Rect left, Rectangle right) { return left.Equals(right); }
        /// <summary>
        /// Tests whether two rectangles are different.
        /// </summary>
        /// <param name="left"> The rect structure on the left side of the equality operator. </param>
        /// <param name="right"> The rectangle structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if left and right have equal X, Y, width and height; otherwise <see langword="false"/>. </returns>
        public static bool operator !=(Rect left, Rectangle right) { return !left.Equals(right); }

        /// <summary>
        /// Tests whether two rectangles are equal.
        /// </summary>
        /// <param name="left"> The rectangle structure on the left side of the equality operator. </param>
        /// <param name="right"> The rect structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if left and right have equal X, Y, width and height; otherwise <see langword="false"/>. </returns>
        public static bool operator ==(Rectangle left, Rect right) { return right.Equals(left); }
        /// <summary>
        /// Tests whether two rectangles are different.
        /// </summary>
        /// <param name="left"> The rectangle structure on the left side of the equality operator. </param>
        /// <param name="right"> The rect structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if left and right have equal X, Y, width and height; otherwise <see langword="false"/>. </returns>
        public static bool operator !=(Rectangle left, Rect right) { return !right.Equals(left); }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> struct with specified parameters.
        /// </summary>
        /// <param name="x"> The x-coordinate of the rectangle. </param>
        /// <param name="y"> The y-coordinate of the rectangle. </param>
        /// <param name="width"> The width of the rectangle. </param>
        /// <param name="height"> The height of the rectangle. </param>
        public Rect(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> struct width a specified position and size. 
        /// </summary>
        /// <param name="position"> The coordiantes of the rectangle. </param>
        /// <param name="size"> The size of the rectangle. </param>
        public Rect(Vector2 position, Size size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> struct from a xna <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle"> The xna style rectangle that contains the values. </param>
        public Rect(Rectangle rectangle)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

        /// <summary>
        /// Determines whether this <see cref="Rect"/> entirely contains a specified <see cref="Rect"/>.
        /// </summary>
        /// <param name="value"> The rectangle to evaluate. </param>
        /// <returns> <see langword="true"/> if value is fully inside the rectangle; otherwise, <see langword="false"/>. </returns>
        public bool Contains(Rect value)
        {
            return Left <= value.Left && Right >= value.Right && Top <= value.Top && Bottom >= value.Bottom;
        }

        /// <summary>
        /// Determines whether this <see cref="Rect"/> contains a specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value"> the coordinate to evaluate. </param>
        /// <returns> <see langword="true"/> if value is inside the rectangle; otherwise, <see langword="false"/>. </returns>
        public bool Contains(Vector2 value)
        {
            return Contains(value.X, value.Y);
        }

        /// <summary>
        /// Determines whether this <see cref="Rect"/> contains a specified coordinate.
        /// </summary>
        /// <param name="x"> The x-coordinate. </param>
        /// <param name="y"> The y-coordinate. </param>
        /// <returns> <see langword="true"/> if the coordinate is inside the rectangle; otherwise, <see langword="false"/>. </returns>
        public bool Contains(float x, float y)
        {
            return Left <= x && Right >= x && Top <= y && Bottom >= y;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj"> The object to compare with the current instance. </param>
        /// <returns> <see langword="true"/> if obj and this instance are the same type and respresent the same value; othersize, <see langword="false"/>. </returns>
        public override bool Equals(object obj)
        {
            Type t = obj.GetType();
            if (t == typeof(Rect)) return Equals((Rect)obj);
            if (t == typeof(Rectangle)) return Equals((Rectangle)obj);
            return false;
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="Rect"/> are equal.
        /// </summary>
        /// <param name="other"> The rectangle to compare with the current instance. </param>
        /// <returns> <see langword="true"/> if other and this instance represent the same value; otherwise, <see langword="false"/>. </returns>
        public bool Equals(Rect other)
        {
            return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="Rectangle"/> are equal.
        /// </summary>
        /// <param name="other"> The rectangle to compare with the current instance. </param>
        /// <returns> <see langword="true"/> if other and this instance represent the same value; otherwise, <see langword="false"/>. </returns>
        public bool Equals(Rectangle other)
        {
            return other.X == (int)X && other.Y == (int)Y && other.Width == Width && other.Height == Height;
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Rect"/> structure.
        /// </summary>
        /// <returns> An integer value that specifies a hash value for this <see cref="Rect"/> structure. </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = HASH_BASE;
                hash += ComputeHash(hash, X);
                hash += ComputeHash(hash, Y);
                hash += ComputeHash(hash, Width);
                hash += ComputeHash(hash, Height);
                return hash;
            }
        }

        /// <summary>
        /// Pushes the edges of the rectangle out by the horizontal and vertical values specified.
        /// </summary>
        /// <param name="horizontal"> Value to push the sides out by. </param>
        /// <param name="vertical"> Value to push the top and bottom out by. </param>
        public void Inflate(int horizontal, int vertical)
        {
            horizontal /= 2;
            vertical /= 2;

            if (Width > 0)
            {
                X -= horizontal;
                Width += horizontal;
            }
            else
            {
                X += horizontal;
                Width -= horizontal;
            }

            if(Height > 0)
            {
                Y -= vertical;
                Height += horizontal;
            }
        }

        /// <summary>
        /// Creates a <see cref="Rect"/> defining the area where one <see cref="Rect"/> overlaps with another <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect1"> The first rectangle to compare. </param>
        /// <param name="rect2"> The second rectangle to compare. </param>
        /// <returns> A <see cref="Rect"/> that represents the overlap between rect1 and rect2. </returns>
        public static Rect Intersection(Rect rect1, Rect rect2)
        {
            float xl = Max(rect1.Left, rect2.Left);
            float xs = Min(rect1.Right, rect2.Right);
            if (xs < xl) return Empty;

            float yl = Max(rect1.Bottom, rect2.Bottom);
            float ys = Min(rect1.Top, rect2.Top);
            if (ys < yl) return Empty;

            return new Rect(xl, ys, (int)(xs - xl), (int)(yl - ys));
        }

        /// <summary>
        /// Determines whether a specified <see cref="Rect"/> intersects with this <see cref="Rect"/>.
        /// </summary>
        /// <param name="value"> The rectangle to evaluate. </param>
        /// <returns> <see langword="true"/> if value intersect the rectangle; otherwise, <see langword="false"/>. </returns>
        public bool Intersects(Rect value)
        {
            float xl = Max(Left, value.Left);
            float xs = Min(Right, value.Right);
            if (xs < xl) return false;

            float yl = Max(Bottom, value.Bottom);
            float ys = Min(Top, value.Top);
            if (ys < yl) return false;

            return true;
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Rect"/> structure.
        /// </summary>
        /// <returns> A string that represents this <see cref="Rect"/>. </returns>
        public override string ToString()
        {
            return $"{{X:{X}, Y:{Y}, Width:{Width}, Height:{Height}}}";
        }

        /// <summary>
        /// Converts this <see cref="Rect"/> to a <see cref="Rectangle"/>.
        /// </summary>
        /// <returns> A <see cref="Rectangle"/> representing this instance. </returns>
        public Rectangle ToXnaRectangle()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }

        /// <summary>
        /// Creates a <see cref="Rect"/> that exactly contains two other <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect1"> The first rectangle to contain. </param>
        /// <param name="rect2"> The second rectangle to contain. </param>
        /// <returns> A rectangle that contains both input rectangles. </returns>
        public static Rect Union(Rect rect1, Rect rect2)
        {
            float bottom = Max(rect1.Bottom, rect2.Bottom);
            float left = Min(rect1.Left, rect2.Left);
            float right = Max(rect1.Right, rect2.Right);
            float top = Min(rect1.Top, rect2.Top);

            return new Rect(left, top, (int)(right - left), (int)(bottom - top));
        }
    }
}