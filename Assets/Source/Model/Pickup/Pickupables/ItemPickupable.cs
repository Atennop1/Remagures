using System;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.Pickup
{
    public sealed class ItemPickupable : IPickupable
    {
        private readonly IInventory<IItem> _inventory;
        private readonly IItem _item;

        public ItemPickupable(IInventory<IItem> inventory, IItem item)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public void Pickup()
            => _inventory.Add(new Cell<IItem>(_item));
    }
}