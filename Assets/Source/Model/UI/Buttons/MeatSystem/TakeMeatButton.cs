using System;
using Remagures.Model.MeatSystem;

namespace Remagures.Model.UI
{
    public sealed class TakeMeatButton : IButton
    {
        private readonly ICookedMeatHeap _cookedMeatHeap;

        public TakeMeatButton(ICookedMeatHeap cookedMeatHeap) 
            => _cookedMeatHeap = cookedMeatHeap ?? throw new ArgumentNullException(nameof(cookedMeatHeap));

        public void Press() 
            => _cookedMeatHeap.Take(_cookedMeatHeap.Count);
    }
}