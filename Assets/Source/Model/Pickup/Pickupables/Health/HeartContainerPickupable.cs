using System;
using Remagures.Model.Health;

namespace Remagures.Model.Pickup
{
    public sealed class HeartContainerPickupable : IPickupable
    {
        private const int HEALTH_POINTS_IN_HEART = 4;
        
        private readonly IMaxHealth _maxPlayerHealth;
        private readonly IHealth _health;

        public HeartContainerPickupable(IHealth health, IMaxHealth maxHealth)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _maxPlayerHealth = maxHealth ?? throw new ArgumentNullException(nameof(maxHealth));
        }

        public void Pickup()
        {
            if (!_maxPlayerHealth.CanIncrease(HEALTH_POINTS_IN_HEART))
                return;
            
            _maxPlayerHealth.Increase(HEALTH_POINTS_IN_HEART);
            _health.Heal(_health.Max.Value - _health.CurrentValue);
        }
    }
}
