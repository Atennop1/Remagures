using System;
using Remagures.Model.MeatSystem;

namespace Remagures.Model.UI
{
    public sealed class AddMeatButton : IButton
    {
        private readonly IMeatCooker _meatCooker;

        public AddMeatButton(IMeatCooker meatCooker) 
            => _meatCooker = meatCooker ?? throw new ArgumentNullException(nameof(meatCooker));

        public void Press() 
            => _meatCooker.AddRawMeat();
    }
}