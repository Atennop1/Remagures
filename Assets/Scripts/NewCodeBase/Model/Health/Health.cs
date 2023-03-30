using System;
using Remagures.Tools;

namespace Remagures.Model.Health
{
    public sealed class Health : IHealth
    {
        public int CurrentValue { get; private set; }
        public IReadOnlyMaxHealth Max { get; }
        
        public bool IsDead => CurrentValue <= 0;
        public bool CanTakeDamage => !IsDead;

        public Health(IReadOnlyMaxHealth maxHealth)
        {
            Max = maxHealth ?? throw new ArgumentNullException(nameof(maxHealth));
            CurrentValue = maxHealth.Value.ThrowExceptionIfLessOrEqualsZero();
        }

        public void TakeDamage(int count)
        {
            if (IsDead)
                throw new Exception("Can't take damage to dead health");
            
            CurrentValue -= count.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Heal(int count)
        {
            if (IsDead)
                throw new ArgumentException("Can't heal a dead health!");
            
            if (CurrentValue + count > Max.Value)
                count = Max.Value - CurrentValue;

            CurrentValue += count.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}