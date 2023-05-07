using System;
using Remagures.Tools;
using Remagures.View.MeatSystem;

namespace Remagures.Model.MeatSystem
{
    public sealed class MeatCooker : IMeatCooker
    {
        public int RawMeatCount { get; private set; }

        private readonly MeatCountView _meatCountView;
        private readonly ICookedMeatHeap _cookedMeatHeap;

        public MeatCooker(MeatCountView meatCountView, ICookedMeatHeap cookedMeatHeap)
        {
            _meatCountView = meatCountView ?? throw new ArgumentNullException(nameof(meatCountView));
            _cookedMeatHeap = cookedMeatHeap ?? throw new ArgumentNullException(nameof(cookedMeatHeap));
        }

        public void CookMeat(int count)
        {
            if (count.ThrowExceptionIfLessOrEqualsZero() > RawMeatCount)
                throw new ArgumentException("Can't cook more meat than have");
            
            RawMeatCount -= count;
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
            _cookedMeatHeap.Add(count);
        }
        
        public void AddRawMeat(int count)
        {
            RawMeatCount += count.ThrowExceptionIfLessOrEqualsZero();
            _meatCountView.DisplayRawMeatCount(RawMeatCount);
        }
    }
}