using System;

namespace Mentula.GuiItems.Core
{
    public struct ProgresData
    {
        public int Minimum { get { return min; } set { if (value < max) min = value; else throw new ArgumentException("The minimum must be higher than the maximum!"); } }
        public int Maximum { get { return max; } set { if (value > min) max = value; else throw new ArgumentException("The maximum must be higher than the minimum!"); } }
        public int Value { get { return val; } set { if (value <= max && value >= min) val = value; } }

        internal float OnePercent { get { return (float)Distance / 100; } }
        internal int Distance { get { return max - min; } }

        private int min;
        private int max;
        private int val;

        public ProgresData(int value)
        {
            min = 0;
            max = 100;

            if (value < min || value > max) throw new ArgumentException("Value must be between the minimum and the maximum.");

            val = value;
        }

        public ProgresData(int minimum, int maximum)
        {
            min = minimum;
            max = maximum;
            val = minimum;
        }

        public ProgresData(int minimum, int maximum, int value)
        {
            if (value < minimum || value > maximum) throw new ArgumentException("Value must be between the minimum and the maximum.");

            min = minimum;
            max = maximum;
            val = value;
        }

        public void ChangeValue(int percent)
        {
            int newValue = (int)(percent * OnePercent);

            if (newValue < max && percent > min) val = newValue;
            else if (newValue < max) val = min;
            else val = max;
        }
    }
}
