using System;
using SaveSystem;

namespace Remagures.Model.Health
{
    public sealed class MaxHealthWithSaving : IMaxHealth
    {
        public int Value => _maxHealth.Value;
        
        private readonly IMaxHealth _maxHealth;
        private readonly ISaveStorage<int> _storage;

        public MaxHealthWithSaving(IMaxHealth maxHealth, ISaveStorage<int> storage)
        {
            _maxHealth = maxHealth ?? throw new ArgumentNullException(nameof(maxHealth));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public bool CanIncrease(int amount) 
            => _maxHealth.CanIncrease(amount);

        public bool CanDecrease(int amount) 
            => _maxHealth.CanDecrease(amount);

        public void Increase(int amount)
        {
            _maxHealth.Increase(amount);
            _storage.Save(Value);
        }

        public void Decrease(int amount)
        {
            _maxHealth.Decrease(amount);
            _storage.Save(Value);
        }
    }
}