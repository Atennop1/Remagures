using System;
using Remagures.Tools;

namespace Remagures.Model.Magic
{
    public sealed class MaxMana : IMaxMana
    {
        public int Value { get; private set; }
        private readonly int _maxPossibleValue;

        public MaxMana(int value, int maxPossibleValue)
        {
            if (_maxPossibleValue.ThrowExceptionIfLessOrEqualsZero() < value.ThrowExceptionIfLessOrEqualsZero())
                throw new InvalidOperationException("MaxPossibleValue can't be less than current value");

            _maxPossibleValue = maxPossibleValue;
            Value = value;
        }

        public bool CanIncrease(int amount)
            => Value + amount <= _maxPossibleValue;

        public void Increase(int amount)
        {
            if (!CanIncrease(amount.ThrowExceptionIfLessOrEqualsZero()))
                throw new InvalidOperationException("Value is too big");

            Value += amount;
        }
        
        public bool CanDecrease(int amount)
            => Value - amount > 0;

        public void Decrease(int amount)
        {
            if (CanDecrease(amount.ThrowExceptionIfLessOrEqualsZero()))
                throw new InvalidOperationException("Value is too big");

            Value -= amount;
        }
    }
}