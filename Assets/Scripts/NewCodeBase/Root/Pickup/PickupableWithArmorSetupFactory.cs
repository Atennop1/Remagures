using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PickupableWithArmorSetupFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IPickupableFactory _pickupableFactory;
        [SerializeField] private IArmorSetuper _armorSetuper;

        public IPickupable Create()
            => new PickupableWithArmorSetup(_pickupableFactory.Create(), _armorSetuper);
    }
}