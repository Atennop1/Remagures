using System;
using Remagures.Model.InventorySystem;
using Remagures.Model.MeatSystem;

namespace Remagures.Model.UI
{
    public sealed class TakeMeatButton : IButton
    {
        private readonly ICookedMeatHeap _cookedMeatHeap;
        private readonly IInventory<IUsableItem> _inventory;
        private readonly IUsableItem _cookedMeatItem;

        public TakeMeatButton(ICookedMeatHeap cookedMeatHeap, IInventory<IUsableItem> inventory, IUsableItem cookedMeatItem)
        {
            _cookedMeatHeap = cookedMeatHeap ?? throw new ArgumentNullException(nameof(cookedMeatHeap));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _cookedMeatItem = cookedMeatItem ?? throw new ArgumentNullException(nameof(cookedMeatItem));
        }

        public void Press()
        {
            if (_cookedMeatHeap.Count <= 0)
                return;
            
            _inventory.Add(new Cell<IUsableItem>(_cookedMeatItem, _cookedMeatHeap.Count));
            _cookedMeatHeap.Take(_cookedMeatHeap.Count);
        }
    }
}