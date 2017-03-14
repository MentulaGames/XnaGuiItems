namespace Mentula.GuiItems.Core.Structs
{
    using System;
    using System.Diagnostics;
    using static Utilities;

    /// <summary> A container for progress data (100% base). </summary>
#if !DEBUG
    [DebuggerStepThrough]
#endif
    [DebuggerDisplay("{ToString()}")]
    public struct ProgressData : IEquatable<ProgressData>
    {
        /// <summary> Gets or sets the minimum of the progress data, must be smaller than the <see cref="Maximum"/>. </summary>
        /// <exception cref="ArgumentException"> The minimum is greater than the current <see cref="Maximum"/>. </exception>
        public int Minimum { get { return min; } set { if (value < max) min = value; else throw new ArgumentException("The minimum must be higher than the maximum!"); } }
        /// <summary> Gets or sets the maximum of the progress data, must be greater than the <see cref="Minimum"/>. </summary>
        /// <exception cref="ArgumentException"> The maximum is lesser than the current <see cref="Minimum"/>. </exception>
        public int Maximum { get { return max; } set { if (value > min) max = value; else throw new ArgumentException("The maximum must be higher than the minimum!"); } }
        /// <summary> Gets or sets a value indicating the current progress position. Value will be clamped. </summary>
        public int Value { get { return val; } set { val = value.Clamp(max, min); } }
        /// <summary> Gets the default maximum of the <see cref="ProgressData"/>. </summary>
        public static int DefaultMaximum { get { return 100; } }
        /// <summary> Gets the default minimum of the <see cref="ProgressData"/>. </summary>
        public static int DefaultMinimum { get { return 0; } }

        internal float OnePercent { get { return (float)Distance / 100; } }
        internal int Distance { get { return max - min; } }

        private int min, max, val;

        /// <summary>
        /// Tests whether two <see cref="ProgressData"/> structures are equal.
        /// </summary>
        /// <param name="pgd1"> The <see cref="ProgressData"/> structure on the left side of the equality operator. </param>
        /// <param name="pgd2"> The <see cref="ProgressData"/> structure on the right side of the equality operator. </param>
        /// <returns> <see langword="true"/> if pgd1 and pgd2 have equal minimum, maximum and value; otherwise <see langword="false"/>. </returns>
        public static bool operator ==(ProgressData pgd1, ProgressData pgd2) { return pgd1.Equals(pgd2); }
        /// <summary>
        /// Tests whether two <see cref="ProgressData"/> structures are different.
        /// </summary>
        /// <param name="pgd1"> The <see cref="ProgressData"/> structure on the left side of the inequality operator. </param>
        /// <param name="pgd2"> The <see cref="ProgressData"/> structure on the right side of the inequality operator. </param>
        /// <returns> <see langword="true"/> if pgd1 and pgd2 differ eather in minimum, maximum or value; otherwise <see langword="false"/>. </returns>
        public static bool operator !=(ProgressData pgd1, ProgressData pgd2) { return !pgd1.Equals(pgd2); }

        /// <summary> Initializes a new instance of the <see cref="ProgressData"/> struct with a specified starting value. </summary>
        /// <param name="value"> The starting value (clamped). </param>
        public ProgressData(int value)
            : this(DefaultMinimum, DefaultMaximum, value)
        { }

        /// <summary> Initializes a new instance of the <see cref="ProgressData"/> struct with a minimum and maximum specified. </summary>
        /// <param name="minimum"> The starting minimum. </param>
        /// <param name="maximum"> The starting maximum. </param>
        public ProgressData(int minimum, int maximum)
            : this(minimum, maximum, minimum)
        { }

        /// <summary> Initializes a new instance of the <see cref="ProgressData"/> struct with a minimum, maximum and starting value specified. </summary>
        /// <param name="minimum"> The starting minimum. </param>
        /// <param name="maximum"> The starting maximum. </param>
        /// <param name="value"> The starting value (clamped). </param>
        public ProgressData(int minimum, int maximum, int value)
        {
            min = minimum;
            max = maximum;
            val = value.Clamp(min, max);
        }

        /// <summary> Changes the current <see cref="Value"/> to a specified percentage. Value will be clamped. </summary>
        /// <param name="percent"> The percentage to change to. </param>
        public void ChangeValue(int percent)
        {
            int newValue = (int)(percent * OnePercent);
            val = newValue.Clamp(max, min);
        }

        /// <summary>
        /// Tests to see whether the specified object is a <see cref="ProgressData"/> structure with the same dimentions as this <see cref="ProgressData"/> structure.
        /// </summary>
        /// <param name="obj"> The Object to test. </param>
        /// <returns> 
        /// <see langword="true"/> if obj is a <see cref="ProgressData"/> 
        /// and has the same minimum, maximum and value as this <see cref="ProgressData"/>; otherwise, <see langword="false"/>. 
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is ProgressData) return Equals((ProgressData)obj);
            return false;
        }

        /// <summary>
        /// Tests to see whether the specified <see cref="ProgressData"/> has the same dimentions as this <see cref="ProgressData"/> structure.
        /// </summary>
        /// <param name="obj"> The <see cref="ProgressData"/> to test. </param>
        /// <returns> <see langword="true"/> if object has the same minimum, maximum and value as this <see cref="ProgressData"/>; otherwise, <see langword="false"/>. </returns>
        public bool Equals(ProgressData obj)
        {
            return obj.min == min
                && obj.max == max
                && obj.val == val;
        }

        /// <summary>
        /// Returns a hash code for this <see cref="ProgressData"/> structure.
        /// </summary>
        /// <returns> An integer value that specifies a hash value for this <see cref="ProgressData"/> structure. </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = HASH_BASE;
                hash += ComputeHash(hash, min);
                hash += ComputeHash(hash, max);
                hash += ComputeHash(hash, val);
                return hash;
            }
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="ProgressData"/> structure.
        /// </summary>
        /// <returns> A string that represents this <see cref="ProgressData"/>. </returns>
        public override string ToString()
        {
            return $"{min}[{val}]{max}";
        }
    }
}