using System;
using Remagures.Tools;
using Remagures.View.Mana;

namespace Remagures.Model.Magic
{
    public sealed class Mana : IMana
    {
        public int CurrentValue { get; private set; }
        public int MaxValue { get; private set; }
        
        private readonly IManaView _view;

        public Mana(IManaView view, int currentValue)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            CurrentValue = currentValue.ThrowExceptionIfLessOrEqualsZero();
            _view.Display(CurrentValue, MaxValue);
        }

        public void Increase(int value)
        {
            if (CurrentValue + value.ThrowExceptionIfLessOrEqualsZero() > MaxValue)
                throw new InvalidOperationException("Increasing value is too big");
            
            CurrentValue += value;
            _view.Display(CurrentValue, MaxValue);
        }

        public void Decrease(int value)
        {
            if (CurrentValue - value.ThrowExceptionIfLessOrEqualsZero() < 0)
                throw new InvalidOperationException("Decreasing value is very big");
            
            CurrentValue -= value;
            _view.Display(CurrentValue, MaxValue);
        }
    }
}
