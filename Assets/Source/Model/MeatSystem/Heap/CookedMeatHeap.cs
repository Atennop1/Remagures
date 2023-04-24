using System;
using Remagures.Model.InventorySystem;
using Remagures.Tools;
using Remagures.View.MeatSystem;

namespace Remagures.Model.MeatSystem
{
    public class CookedMeatHeap : ICookedMeatHeap
    {
        public int Count { get; private set; }
        
        private readonly MeatCountView _meatCountView;
        private readonly IInventory<IItem> _inventory;
        private readonly IItem _cookedMeatItem;

        public CookedMeatHeap(MeatCountView meatCountView, IInventory<IItem> inventory, IItem cookedMeatItem)
        {
            _meatCountView = meatCountView ?? throw new ArgumentNullException(nameof(meatCountView));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _cookedMeatItem = cookedMeatItem ?? throw new ArgumentNullException(nameof(cookedMeatItem));
        }

        public void Add(int count)
        {
            Count += count.ThrowExceptionIfLessOrEqualsZero();
            _meatCountView.DisplayCookedMeatCount(Count);
        }

        public void Take(int count)
        {
            if (Count < count)
                throw new ArgumentException($"Can't take {count} meat when on heap only {Count} meat");
            
            _inventory.Add(new Cell<IItem>(_cookedMeatItem, count));
            _meatCountView.DisplayCookedMeatCount(0);
            Count = 0;
        }
    }
}