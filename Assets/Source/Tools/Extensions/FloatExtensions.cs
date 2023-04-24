using System;

namespace Remagures.Tools
{
    public static class FloatExtensions
    {
        public static float ThrowExceptionIfLessThanZero(this float number)
        {
            if (number < 0)
                throw new ArgumentException("Number can't be less than zero");

            return number;
        }

        public static float ThrowExceptionIfLessOrEqualsZero(this float number)
        {
            if (number == 0)
                throw new ArgumentException("Number can't be zero");

            return number.ThrowExceptionIfLessThanZero();
        }
    }
}