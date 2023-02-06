using System;
using Remagures.Tools;

namespace Remagures.Model.HealthSystem
{
    public sealed class Health : IHealth
    {
        public int CurrentValue { get; private set; }
        public int MaxValue { get; }
        
        public bool IsDead => CurrentValue <= 0;
        public bool CanTakeDamage => !IsDead;

        public Health(int value)
            => MaxValue = CurrentValue = value.ThrowExceptionIfLessThanZero();

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
            
            if (CurrentValue + count > MaxValue)
                count = MaxValue - CurrentValue;

            CurrentValue += count.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}