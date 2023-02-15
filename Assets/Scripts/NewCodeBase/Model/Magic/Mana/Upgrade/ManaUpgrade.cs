using System;
using Remagures.Tools;

namespace Remagures.Model.Magic
{
    public class ManaUpgrade : IMana, IManaUpgrade
    {
        public int MaxPossibleValue { get; }
        public int MaxValue { get; private set; }
        public int CurrentValue => _mana.CurrentValue;
        
        private readonly IMana _mana;

        public ManaUpgrade(IMana mana, int maxPossibleValue)
        {
            _mana = mana ?? throw new ArgumentNullException(nameof(mana));
            MaxPossibleValue = maxPossibleValue.ThrowExceptionIfLessOrEqualsZero();
            MaxValue = _mana.MaxValue;
        }

        public void Increase(int value)
            => _mana.Increase(value);

        public void Decrease(int value)
            => _mana.Decrease(value);

        public void IncreaseMaxMana(int value)
        {
            if (MaxValue + value.ThrowExceptionIfLessOrEqualsZero() > MaxPossibleValue)
                throw new InvalidOperationException("Increasing amount is too big");
            
            MaxValue = _mana.MaxValue + value;
        }
    }
}