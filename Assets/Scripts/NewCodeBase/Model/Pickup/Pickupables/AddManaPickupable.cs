using System;
using Remagures.Model.Magic;
using Remagures.Tools;

namespace Remagures.Model.Pickup
{
    public class AddManaPickupable : IPickupable //TODO make factory for this after mana composite
    {
        private readonly IMana _mana;
        private int _amount;

        public AddManaPickupable(IMana mana, int amount)
        {
            _mana = mana ?? throw new ArgumentNullException(nameof(mana));
            _amount = amount.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Pickup()
        {
            if (_amount + _mana.CurrentValue > _mana.MaxValue)
                _amount = _mana.MaxValue - _mana.CurrentValue;
            
            _mana.Increase(_amount);
        }
    }
}
