using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class ArmorItem : IArmorItem
    {
        public AnimatorOverrideController AnimatorController { get; }
        public int Armor { get; }
        
        public string Name => _item.Name;
        public string Description => _item.Description;
        public Sprite Sprite => _item.Sprite;
        public bool IsStackable => _item.IsStackable;

        private readonly IItem _item;

        public ArmorItem(IItem item, AnimatorOverrideController animatorController, int armor)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            AnimatorController = animatorController ?? throw new ArgumentNullException(nameof(animatorController));
            Armor = armor.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}