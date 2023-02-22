using System.Linq;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.MeatSystem
{
    public sealed class MeatCooker
    {
        public int RawMeatCount { get; private set; }
        private int _cookedMeatCount;

        private readonly MeatCountView _meatCountView;
        private readonly Inventory<Item> _inventory;
        private readonly Item _cookedMeatItem;
        private readonly Item _rawMeatItem;

        public void LootMeat()
        {
            _inventory.Add(new Cell<Item>(_cookedMeatItem, _cookedMeatCount));
            
            _cookedMeatCount = 0;
            _meatCountView.DisplayCookedMeatCount(_cookedMeatCount);
        }

        public void CookOneMeat()
        {
            _cookedMeatCount++;
            RawMeatCount--;
            
            _meatCountView.DisplayCookedMeatCount(_cookedMeatCount);
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
        }

        public void AddRawMeat()
        {
            if (!_inventory.Cells.Any(cell => cell.Item.Equals(_rawMeatItem)))
                return;

            var rawMeatCell = _inventory.Cells.ToList().Find(cell => cell.Item.Equals(_rawMeatItem));
            RawMeatCount += rawMeatCell.ItemsCount;

            _inventory.Decrease(new Cell<Item>(_rawMeatItem, rawMeatCell.ItemsCount));
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
        }
    }
}