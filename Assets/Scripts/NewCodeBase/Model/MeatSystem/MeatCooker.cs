using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Root;
using Remagures.Tools;
using Remagures.View.MeatSystem;

namespace Remagures.Model.MeatSystem
{
    public sealed class MeatCooker : ILateUpdatable
    {
        public int RawMeatCount { get; private set; }
        public bool HasRawMeatAdded { get; private set; }
        private int _cookedMeatCount;

        private readonly MeatCountView _meatCountView;
        private readonly Inventory<Item> _inventory;
        private readonly Item _cookedMeatItem;
        private readonly Item _rawMeatItem;

        public MeatCooker(MeatCountView meatCountView, Inventory<Item> inventory, Item cookedMeatItem, Item rawMeatItem)
        {
            _meatCountView = meatCountView ?? throw new ArgumentNullException(nameof(meatCountView));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _cookedMeatItem = cookedMeatItem;
            _rawMeatItem = rawMeatItem;
        }

        public void LateUpdate()
            => HasRawMeatAdded = false;

        public void CookMeat(int count)
        {
            if (count.ThrowExceptionIfLessOrEqualsZero() > RawMeatCount)
                throw new ArgumentException("Can't cook more meat than have");
            
            _cookedMeatCount += count;
            RawMeatCount -= count;
            
            _meatCountView.DisplayCookedMeatCount(_cookedMeatCount);
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
        }

        public void LootCookedMeat()
        {
            _inventory.Add(new Cell<Item>(_cookedMeatItem, _cookedMeatCount));
            
            _cookedMeatCount = 0;
            _meatCountView.DisplayCookedMeatCount(_cookedMeatCount);
        }
        
        public void AddRawMeat()
        {
            if (!_inventory.Cells.Any(cell => cell.Item.Equals(_rawMeatItem)))
                return;

            HasRawMeatAdded = true;
            var rawMeatCell = _inventory.Cells.ToList().Find(cell => cell.Item.Equals(_rawMeatItem));
            RawMeatCount += rawMeatCell.ItemsCount;

            _inventory.Remove(new Cell<Item>(_rawMeatItem, rawMeatCell.ItemsCount));
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
        }
    }
}