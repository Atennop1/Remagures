using System;

namespace Remagures.Tools
{
    public static class IntExtensions
    {
        public static int ThrowExceptionIfLessThanZero(this int number)
        {
            if (number < 0)
                throw new ArgumentException("Number can't be less than zero");

            return number;
        }

        public static int ThrowExceptionIfLessOrEqualsZero(this int number)
        {
            if (number == 0)
                throw new ArgumentException("Number can't be zero");

            return number.ThrowExceptionIfLessThanZero();
        }
    }
}