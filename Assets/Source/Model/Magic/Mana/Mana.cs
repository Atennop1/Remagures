using System;
using Remagures.Tools;
using Remagures.View.Mana;

namespace Remagures.Model.Magic
{
    public sealed class Mana : IMana
    {
        public int CurrentValue { get; private set; }
        public IReadOnlyMaxMana Max { get; }
        
        private readonly IManaView _view;

        public Mana(IManaView view, IReadOnlyMaxMana max)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            Max = max ?? throw new ArgumentNullException(nameof(max));
            CurrentValue = max.Value;
            _view.Display(CurrentValue, Max.Value);
        }

        public void Increase(int value)
        {
            if (CurrentValue + value.ThrowExceptionIfLessOrEqualsZero() > Max.Value)
                throw new InvalidOperationException("Increasing value is too big");
            
            CurrentValue += value;
            _view.Display(CurrentValue, Max.Value);
        }

        public void Decrease(int value)
        {
            if (CurrentValue - value.ThrowExceptionIfLessOrEqualsZero() < 0)
                throw new InvalidOperationException("Decreasing value is very big");
            
            CurrentValue -= value;
            _view.Display(CurrentValue, Max.Value);
        }
    }
}
