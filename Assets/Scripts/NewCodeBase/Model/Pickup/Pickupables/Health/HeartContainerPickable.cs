using System;
using Remagures.Model.Health;
using Remagures.Model.Health.HealthUpgrade;

namespace Remagures.Model.Pickup
{
    public class HeartContainerPickable : IPickupable
    {
        private const int HEALTH_POINTS_IN_HEART = 4;
        
        private readonly IHealthUpgrade _healthUpgrade;
        private readonly IHealth _health;

        public HeartContainerPickable(IHealth health, IHealthUpgrade healthUpgrade)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _healthUpgrade = healthUpgrade ?? throw new ArgumentNullException(nameof(healthUpgrade));
        }

        public void Pickup()
        {
            _healthUpgrade.IncreaseMaxHealth(HEALTH_POINTS_IN_HEART);
            _health.Heal(_health.MaxValue - _health.CurrentValue);
        }
    }
}
