using System;
using Remagures.Model.Health;

namespace Remagures.Model.Pickup
{
    public class HeartContainerPickupable : IPickupable
    {
        private const int HEALTH_POINTS_IN_HEART = 4;
        
        private readonly IMaxHealth _maxHealth;
        private readonly IHealth _health;

        public HeartContainerPickupable(IHealth health, IMaxHealth maxHealth)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _maxHealth = maxHealth ?? throw new ArgumentNullException(nameof(maxHealth));
        }

        public void Pickup()
        {
            if (!_maxHealth.CanIncrease(HEALTH_POINTS_IN_HEART))
                return;
            
            _maxHealth.Increase(HEALTH_POINTS_IN_HEART);
            _health.Heal(_health.Max.Value - _health.CurrentValue);
        }
    }
}
