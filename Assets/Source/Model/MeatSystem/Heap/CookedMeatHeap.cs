using System;
using Remagures.Model.InventorySystem;
using Remagures.Tools;
using Remagures.View.MeatSystem;

namespace Remagures.Model.MeatSystem
{
    public sealed class CookedMeatHeap : ICookedMeatHeap
    {
        public int Count { get; private set; }
        
        private readonly IMeatCountView _meatCountView;

        public CookedMeatHeap(IMeatCountView meatCountView) 
            => _meatCountView = meatCountView ?? throw new ArgumentNullException(nameof(meatCountView));

        public void Add(int count)
        {
            Count += count.ThrowExceptionIfLessOrEqualsZero();
            _meatCountView.DisplayCookedMeatCount(Count);
        }

        public void Take(int count)
        {
            if (Count < count.ThrowExceptionIfLessOrEqualsZero())
                throw new ArgumentException($"Can't take {count} meat when on heap only {Count} meat");

            Count -= count;
            _meatCountView.DisplayCookedMeatCount(Count);
        }
    }
}