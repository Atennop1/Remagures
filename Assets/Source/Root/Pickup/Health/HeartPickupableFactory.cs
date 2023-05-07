using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Health
{
    public sealed class HeartPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IHealthFactory _healthFactory;
        private IPickupable _builtPickupable;
        
        public IPickupable Create()
        {
            if (_builtPickupable != null)
                return _builtPickupable;

            _builtPickupable = new HeartPickupable(_healthFactory.Create());
            return _builtPickupable;
        }
    }
}