using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls.Util
{
    public static class MathUtil
    {
        /// <summary>
        /// Restricts a value to be within a specified range. 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If value is less than min, min will be returned.</param>
        /// <param name="max">The maximum value. If value is greater than max, max will be returned.</param>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            else if (value > max) return max;
            else return value;
        }

        /// <summary>
        /// Restricts a value to be within a specified range. 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If value is less than min, min will be returned.</param>
        /// <param name="max">The maximum value. If value is greater than max, max will be returned.</param>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            else if (value > max) return max;
            else return value;
        }
    }
}
