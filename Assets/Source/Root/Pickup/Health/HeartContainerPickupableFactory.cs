using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Health
{
    public sealed class HeartContainerPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IHealthFactory _healthFactory;
        [SerializeField] private IMaxHealthFactory _maxHealthFactory;
        private IPickupable _builtPickupable;
        
        public IPickupable Create()
        {
            if (_builtPickupable != null)
                return _builtPickupable;

            _builtPickupable = new HeartContainerPickupable(_healthFactory.Create(), _maxHealthFactory.Create());
            return _builtPickupable;
        }
    }
}