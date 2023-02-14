using System;
using Remagures.Root;
using Remagures.Tools;

namespace Remagures.Model.InventorySystem
{
    public class AddManaListener : IUpdatable
    {
        private readonly IUsableItem _usableItem;
        private readonly IMana _mana;
        private int _amount;

        public AddManaListener(IUsableItem usableItem, IMana mana, int amount)
        {
            _usableItem = usableItem ?? throw new ArgumentNullException(nameof(usableItem));
            _mana = mana ?? throw new ArgumentNullException(nameof(mana));
            _amount = amount.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Update()
        {
            if (!_usableItem.HasUsed)
                return;
            
            if (_amount + _mana.CurrentValue > _mana.MaxValue)
                _amount = _mana.MaxValue - _mana.CurrentValue;
            
            _mana.Increase(_amount);
        }
    }
}
