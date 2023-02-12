using Remagures.Model.InventorySystem;

namespace Remagures.Model.Pickup
{
    public sealed class ItemPickupable<T> : IPickupable where T: IItem
    {
        private readonly IInventory<T> _inventory;
        private readonly IItem _item;

        public void Pickup()
            => _inventory.Add(new Cell<T>((T)_item));
    }
}