using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PickupableWithArmorSetupFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IPickupableFactory _pickupableFactory;
        [SerializeField] private IArmorSetuper _armorSetuper;
        private IPickupable _builtPickupable;

        public IPickupable Create()
        {
            if (_builtPickupable != null)
                return _builtPickupable;

            _builtPickupable = new PickupableWithArmorSetup(_pickupableFactory.Create(), _armorSetuper);
            return _builtPickupable;
        }
    }
}