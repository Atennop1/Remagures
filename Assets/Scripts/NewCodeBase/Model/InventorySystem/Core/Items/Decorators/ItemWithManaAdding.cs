using System;
using Remagures.Model.Magic;
using Remagures.Root;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class ItemWithManaAdding : IUsableItem
    {
        public string Name => _usableItem.Name;
        public string Description => _usableItem.Description;
        public Sprite Sprite => _usableItem.Sprite;
        public bool IsStackable => _usableItem.IsStackable;
        
        private readonly IUsableItem _usableItem;
        private readonly IMana _mana;
        private int _amount;

        public ItemWithManaAdding(IUsableItem usableItem, IMana mana, int amount)
        {
            _usableItem = usableItem ?? throw new ArgumentNullException(nameof(usableItem));
            _mana = mana ?? throw new ArgumentNullException(nameof(mana));
            _amount = amount.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Use()
        {
            _usableItem.Use();
            
            if (_amount + _mana.CurrentValue > _mana.MaxValue)
                _amount = _mana.MaxValue - _mana.CurrentValue;
            
            _mana.Increase(_amount);
        }
    }
}
