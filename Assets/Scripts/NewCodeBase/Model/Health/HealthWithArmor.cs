using System;

namespace Remagures.Model.Health
{
    public sealed class HealthWithArmor : IHealth
    {
        public int MaxValue => _health.MaxValue;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;

        private readonly IHealth _health;
        private readonly IArmor _armor;

        public HealthWithArmor(IHealth health, IArmor armor)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _armor = armor ?? throw new ArgumentNullException(nameof(armor));
        }

        public void TakeDamage(int amount)
            => _health.TakeDamage(_armor.AbsorbDamage(amount));

        public void Heal(int amount)
            => _health.Heal(amount);
    }
}