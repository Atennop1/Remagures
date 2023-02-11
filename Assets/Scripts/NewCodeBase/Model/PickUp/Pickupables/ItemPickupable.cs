using Remagures.Model.InventorySystem;

namespace Remagures.Model.PickUp
{
    public sealed class ItemPickupable : IPickupable
    {
        private readonly IInventory _inventory;
        private readonly IItem _item;

        public void Pickup()
            => _inventory.Add(new Cell(_item));
    }
}