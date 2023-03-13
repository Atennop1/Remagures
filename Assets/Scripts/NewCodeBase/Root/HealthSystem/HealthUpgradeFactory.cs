using Remagures.Model.Health.HealthUpgrade;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HealthUpgradeFactory : MonoBehaviour
    {
        [SerializeField] private int _maxPossibleHealth;
        [SerializeField] private HealthFactory _healthFactory;
        private IHealthUpgrade _builtUpgrade;

        public IHealthUpgrade Create()
        {
            if (_builtUpgrade != null)
                return _builtUpgrade;

            _builtUpgrade = new HealthUpgrade(_healthFactory.Create(), _maxPossibleHealth);
            return _builtUpgrade;
        }
    }
}