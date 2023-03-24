using System;
using Remagures.Tools;

namespace Remagures.Model.Health
{
    public sealed class UpgradableHealth : IUpgradableHealth
    {
        public int MaxValue { get; private set; }
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;

        private readonly IHealth _health;
        private readonly int _maxPossibleHealth;
        private int _overHealth;

        public UpgradableHealth(IHealth health, int maxPossibleHealth)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _maxPossibleHealth = maxPossibleHealth.ThrowExceptionIfLessOrEqualsZero();
            MaxValue = _health.MaxValue;
        }

        public void TakeDamage(int amount)
        {
            if (_overHealth > 0)
                _overHealth -= amount.ThrowExceptionIfLessOrEqualsZero();

            if (_overHealth < 0)
            {
                _health.TakeDamage(-_overHealth);
                return;
            }
            
            _health.TakeDamage(amount);
        }

        public void Heal(int amount)
        {
            if (_health.CurrentValue != _health.MaxValue)
            {
                var healedHealth = _health.MaxValue - _health.CurrentValue;
                _health.Heal(amount);
                amount -= healedHealth;
            }
            
            if (amount <= 0)
                return;
            
            if (_overHealth + amount > _maxPossibleHealth)
                amount = _maxPossibleHealth - _overHealth;
            
            _overHealth += amount.ThrowExceptionIfLessOrEqualsZero();
        }

        public bool CanUpgrade(int value)
            => MaxValue + value.ThrowExceptionIfLessOrEqualsZero() <= _maxPossibleHealth;

        public void UpgradeMaxHealth(int value)
        {
            if (!CanUpgrade(value))
                throw new InvalidOperationException("Increasing amount is too big");
            
            MaxValue += value;
            _overHealth += value;
        }
    }
}