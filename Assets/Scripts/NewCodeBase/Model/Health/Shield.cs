using System;
using Remagures.Tools;

namespace Remagures.Model.Health
{
    public sealed class Shield : IHealth
    {
        public int MaxValue => _health.MaxValue;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;

        private readonly IHealth _health;
        private readonly float _armorCoefficient;

        public Shield(IHealth health, float armorCoefficient)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _armorCoefficient = armorCoefficient.ThrowExceptionIfLessOrEqualsZero();
        }

        public void TakeDamage(int amount)
        {
            var totalDamage = (int)(amount * _armorCoefficient + 0.5f);

            if (totalDamage == 0)
                totalDamage = 1;
            
            _health.TakeDamage(totalDamage);
        }

        public void Heal(int amount)
            => _health.Heal(amount);
    }
}