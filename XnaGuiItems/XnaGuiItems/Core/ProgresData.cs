using System;

namespace Mentula.GuiItems.Core
{
    /// <summary> A container for progress data (100% base). </summary>
    public struct ProgressData
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
    }
}