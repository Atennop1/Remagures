using System;
using Remagures.Tools;
using Remagures.View.Health;

namespace Remagures.Model.Health
{
    public sealed class Health : IHealth
    {
        public int CurrentValue { get; private set; }
        public IReadOnlyMaxHealth Max { get; }
        
        public bool IsDead => CurrentValue <= 0;
        public bool CanTakeDamage => !IsDead;

        private readonly IHealthView _healthView;

        public Health(IHealthView healthView, IReadOnlyMaxHealth maxHealth)
        {
            _healthView = healthView ?? throw new ArgumentNullException(nameof(healthView));
            Max = maxHealth ?? throw new ArgumentNullException(nameof(maxHealth));
            CurrentValue = maxHealth.Value.ThrowExceptionIfLessOrEqualsZero();
        }

        public void TakeDamage(int count)
        {
            if (IsDead)
                throw new Exception("Can't take damage to dead health");
            
            CurrentValue -= count.ThrowExceptionIfLessOrEqualsZero();
            _healthView.Display(CurrentValue, Max.Value);
        }

        public void Heal(int count)
        {
            if (IsDead)
                throw new ArgumentException("Can't heal a dead health!");
            
            if (CurrentValue + count > Max.Value)
                count = Max.Value - CurrentValue;

            CurrentValue += count.ThrowExceptionIfLessOrEqualsZero();
            _healthView.Display(CurrentValue, Max.Value);
        }
    }
}