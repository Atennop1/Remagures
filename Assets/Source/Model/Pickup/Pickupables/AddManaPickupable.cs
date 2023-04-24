using System;
using Remagures.Model.Magic;
using Remagures.Tools;

namespace Remagures.Model.Pickup
{
    public class AddManaPickupable : IPickupable
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
            if (_amount + _mana.CurrentValue > _mana.Max.Value)
                _amount = _mana.Max.Value - _mana.CurrentValue;
            
            _mana.Increase(_amount);
        }
    }
}
