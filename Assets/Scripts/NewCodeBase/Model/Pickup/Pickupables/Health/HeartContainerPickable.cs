using System;
using Remagures.Model.Health;

namespace Remagures.Model.Pickup
{
    public class HeartContainerPickable : IPickupable
    {
        private const int HEALTH_POINTS_IN_HEART = 4;
        private readonly PlayerHealth _playerHealth;

        public HeartContainerPickable(PlayerHealth playerHealth)
            => _playerHealth = playerHealth ?? throw new ArgumentNullException(nameof(playerHealth));

        public void Pickup()
            => _playerHealth.IncreaseMaxValue(HEALTH_POINTS_IN_HEART);
    }
}
