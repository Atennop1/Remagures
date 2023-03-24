using System;
using SaveSystem;

namespace Remagures.Model.Health
{
    public sealed class UpgradableHealthWithSaving : IUpgradableHealth
    {
        public int MaxValue => _upgradableHealth.MaxValue;
        public int CurrentValue => _upgradableHealth.CurrentValue;

        public bool IsDead => _upgradableHealth.IsDead;
        public bool CanTakeDamage => _upgradableHealth.CanTakeDamage;
        
        private readonly IUpgradableHealth _upgradableHealth;
        private readonly ISaveStorage<int> _saveStorage;

        public UpgradableHealthWithSaving(IUpgradableHealth upgradableHealth, ISaveStorage<int> saveStorage)
        {
            _upgradableHealth = upgradableHealth ?? throw new ArgumentNullException(nameof(upgradableHealth));
            _saveStorage = saveStorage ?? throw new ArgumentNullException(nameof(saveStorage));
            
            if (_saveStorage.HasSave())
                _upgradableHealth.UpgradeMaxHealth(_saveStorage.Load());
        }

        public void TakeDamage(int amount) 
            => _upgradableHealth.TakeDamage(amount);

        public void Heal(int amount) 
            => _upgradableHealth.Heal(amount);

        public bool CanUpgrade(int value) 
            => _upgradableHealth.CanUpgrade(value);

        public void UpgradeMaxHealth(int value)
        {
            _upgradableHealth.UpgradeMaxHealth(value);
            _saveStorage.Save(MaxValue);
        }
    }
}