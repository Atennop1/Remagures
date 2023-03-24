using Remagures.Model.Health;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UpgradableHealthFactory : MonoBehaviour, IHealthFactory, IUpgradableHealthFactory
    {
        [SerializeField] private int _maxPossibleHealth;
        [SerializeField] private HealthFactory _healthFactory;
        private IUpgradableHealth _builtHealth;

        IHealth IHealthFactory.Create()
            => Create();
        
        public IUpgradableHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;

            _builtHealth = new UpgradableHealth(_healthFactory.Create(), _maxPossibleHealth);
            return _builtHealth;
        }
    }
}