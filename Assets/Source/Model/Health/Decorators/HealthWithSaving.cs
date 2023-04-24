using System;
using SaveSystem;

namespace Remagures.Model.Health
{
    public sealed class HealthWithSaving : IHealth
    {
        public IReadOnlyMaxHealth Max => _health.Max;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;
        
        private readonly IHealth _health;
        private readonly ISaveStorage<int> _storage;

        public HealthWithSaving(IHealth health, ISaveStorage<int> storage)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            
            if (_storage.HasSave())
                _health.Heal(_storage.Load());
        }

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
            _storage.Save(CurrentValue);
        }

        public void Heal(int amount)
        {
            _health.Heal(amount);
            _storage.Save(CurrentValue);
        }
    }
}