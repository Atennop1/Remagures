using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class WeaponItem : IWeaponItem
    {
        public AnimatorOverrideController AnimatorController { get; }
        
        public int Damage { get; }
        public int UsingCooldownInMilliseconds { get; }
        public int AttackingTimeInMilliseconds { get; }

        public string Name => _item.Name;
        public string Description => _item.Description;
        public Sprite Sprite => _item.Sprite;
        public bool IsStackable => _item.IsStackable;

        private readonly IItem _item;

        public WeaponItem(IItem item, AnimatorOverrideController animatorController, int damage, int usingCooldown, int attackingTime)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            AnimatorController = animatorController ?? throw new ArgumentNullException(nameof(animatorController));
            
            Damage = damage.ThrowExceptionIfLessOrEqualsZero();
            UsingCooldownInMilliseconds = usingCooldown.ThrowExceptionIfLessOrEqualsZero();
            AttackingTimeInMilliseconds = attackingTime.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}
