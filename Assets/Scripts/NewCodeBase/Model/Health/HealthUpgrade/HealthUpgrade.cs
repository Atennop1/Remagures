using System;
using Remagures.Tools;

namespace Remagures.Model.Health.HealthUpgrade
{
    public sealed class HealthUpgrade : IHealth, IHealthUpgrade
    {
        public int MaxPossibleHealth { get; }
        public int MaxValue { get; private set; }
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;

        private readonly IHealth _health;

        public HealthUpgrade(IHealth health, int maxPossibleHealth)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            MaxPossibleHealth = maxPossibleHealth.ThrowExceptionIfLessOrEqualsZero();
            MaxValue = _health.MaxValue;
        }

        public void TakeDamage(int amount)
            => _health.TakeDamage(amount);

        public void Heal(int amount)
            => _health.Heal(amount);

        public void IncreaseMaxHealth(int amount)
        {
            if (MaxValue + amount.ThrowExceptionIfLessOrEqualsZero() > MaxPossibleHealth)
                throw new InvalidOperationException("Increasing amount is too big");
            
            MaxValue += amount;
        }
    }
}