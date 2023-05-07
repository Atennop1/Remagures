using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Model.MeatSystem;

namespace Remagures.Model.Buttons
{
    public sealed class AddRawMeatButton : IButton
    {
        private readonly IMeatCooker _meatCooker;
        private readonly IInventory<IItem> _inventory;
        private readonly IItem _rawMeatItem;

        public AddRawMeatButton(IMeatCooker meatCooker, IInventory<IItem> inventory, IItem rawMeatItem)
        {
            _meatCooker = meatCooker ?? throw new ArgumentNullException(nameof(meatCooker));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _rawMeatItem = rawMeatItem ?? throw new ArgumentNullException(nameof(rawMeatItem));
        }

        public void Press()
        {
            if (!_inventory.Cells.Any(cell => cell.Item.Equals(_rawMeatItem)))
                return;
            
            var rawMeatCell = _inventory.Cells.ToList().Find(cell => cell.Item.Equals(_rawMeatItem));
            _inventory.Remove(new Cell<IItem>(_rawMeatItem, rawMeatCell.ItemsCount));
            _meatCooker.AddRawMeat(rawMeatCell.ItemsCount);
        }
    }
}