using Remagures.Model.InventorySystem;
using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ItemPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IInventoryFactory<IItem> _inventoryFactory;
        [SerializeField] private IItemFactory<IItem> _itemFactory;
        private IPickupable _builtPickupable;

        public IPickupable Create()
        {
            if (_builtPickupable != null)
                return _builtPickupable;

            _builtPickupable = new ItemPickupable(_inventoryFactory.Create(), _itemFactory.Create());
            return _builtPickupable;
        }
    }
}