using System;
using Remagures.Model.Health;

namespace Remagures.Model.Pickup
{
    public class HeartContainerPickupable : IPickupable
    {
        private const int HEALTH_POINTS_IN_HEART = 4;
        
        private readonly IUpgradableHealth _upgradableHealth;
        private readonly IHealth _health;

        public HeartContainerPickupable(IHealth health, IUpgradableHealth upgradableHealth)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _upgradableHealth = upgradableHealth ?? throw new ArgumentNullException(nameof(upgradableHealth));
        }

        public void Pickup()
        {
            if (!_upgradableHealth.CanUpgrade(HEALTH_POINTS_IN_HEART))
                return;
            
            _upgradableHealth.UpgradeMaxHealth(HEALTH_POINTS_IN_HEART);
            _health.Heal(_health.MaxValue - _health.CurrentValue);
        }
    }
}
