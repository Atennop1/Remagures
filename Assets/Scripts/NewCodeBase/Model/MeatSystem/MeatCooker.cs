using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Root;
using Remagures.Tools;
using Remagures.View.MeatSystem;

namespace Remagures.Model.MeatSystem
{
    public sealed class MeatCooker : IMeatCooker
    {
        public int RawMeatCount { get; private set; }
        private int _cookedMeatCount;

        private readonly MeatCountView _meatCountView;
        private readonly IInventory<IItem> _inventory;
        private readonly IItem _cookedMeatItem;
        private readonly IItem _rawMeatItem;

        public MeatCooker(MeatCountView meatCountView, IInventory<IItem> inventory, IItem cookedMeatItem, IItem rawMeatItem)
        {
            _meatCountView = meatCountView ?? throw new ArgumentNullException(nameof(meatCountView));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _cookedMeatItem = cookedMeatItem ?? throw new ArgumentNullException(nameof(cookedMeatItem));
            _rawMeatItem = rawMeatItem ?? throw new ArgumentNullException(nameof(rawMeatItem));
        }

        public void CookMeat(int count)
        {
            if (count.ThrowExceptionIfLessOrEqualsZero() > RawMeatCount)
                throw new ArgumentException("Can't cook more meat than have");
            
            _cookedMeatCount += count;
            RawMeatCount -= count;
            
            _meatCountView.DisplayCookedMeatCount(_cookedMeatCount);
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
        }

        public void RemoveCookedMeat()
        {
            _inventory.Add(new Cell<IItem>(_cookedMeatItem, _cookedMeatCount));
            
            _cookedMeatCount = 0;
            _meatCountView.DisplayCookedMeatCount(_cookedMeatCount);
        }
        
        public void AddRawMeat()
        {
            if (!_inventory.Cells.Any(cell => cell.Item.Equals(_rawMeatItem)))
                return;
            
            var rawMeatCell = _inventory.Cells.ToList().Find(cell => cell.Item.Equals(_rawMeatItem));
            RawMeatCount += rawMeatCell.ItemsCount;

            _inventory.Remove(new Cell<IItem>(_rawMeatItem, rawMeatCell.ItemsCount));
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
        }
    }
}