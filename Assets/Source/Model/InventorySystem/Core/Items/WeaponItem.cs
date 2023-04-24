using System;
using Remagures.Model.Attacks;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class WeaponItem : IWeaponItem
    {
        public int Damage { get; }
        public IAttack Attack { get; }
        
        public AnimatorOverrideController AnimatorController { get; }
        
        public string Name => _item.Name;
        public string Description => _item.Description;
        public Sprite Sprite => _item.Sprite;
        public bool IsStackable => _item.IsStackable;

        private readonly IItem _item;

        public WeaponItem(IItem item, AnimatorOverrideController animatorController, IAttack attack, int damage)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            AnimatorController = animatorController ?? throw new ArgumentNullException(nameof(animatorController));
            
            Damage = damage.ThrowExceptionIfLessOrEqualsZero();
            Attack = attack ?? throw new ArgumentNullException(nameof(attack));
        }
    }
}
