using System;
using Remagures.Model.Health;

namespace Remagures.Model.Pickup
{
    public class HeartPickable : IPickupable
    {
        private const int HEALTH_POINTS_IN_HEART = 4;
        private readonly IHealth _playerHealth;

        public HeartPickable(IHealth playerHealth)
            => _playerHealth = playerHealth ?? throw new ArgumentNullException(nameof(playerHealth));

        public void Pickup()
            => _playerHealth.Heal(HEALTH_POINTS_IN_HEART);
    }
}