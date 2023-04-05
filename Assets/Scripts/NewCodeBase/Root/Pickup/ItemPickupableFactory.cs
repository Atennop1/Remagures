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

        public IPickupable Create()
            => new ItemPickupable(_inventoryFactory.Create(), _itemFactory.Create());
    }
}