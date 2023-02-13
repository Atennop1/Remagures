using System;
using Remagures.Tools;

namespace Remagures.Model.Health
{
    public class PlayerHealth : IHealth
    {
        public int MaxValue { get; private set; }
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;

        private readonly IHealth _health;
        private const int MAX_POSSIBLE_VALUE = 40;

        public PlayerHealth(IHealth health)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            MaxValue = _health.MaxValue;
        }

        public void TakeDamage(int amount)
            => _health.TakeDamage(amount);

        public void Heal(int amount)
            => _health.Heal(amount);

        public void IncreaseMaxValue(int amount)
        {
            MaxValue += amount.ThrowExceptionIfLessOrEqualsZero();

            if (MaxValue > MAX_POSSIBLE_VALUE)
                MaxValue = MAX_POSSIBLE_VALUE;
        }
    }
}