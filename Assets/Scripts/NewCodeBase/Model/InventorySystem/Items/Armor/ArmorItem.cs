using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public readonly struct ArmorItem : IArmorItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        
        public AnimatorOverrideController AnimatorController { get; }
        public int Armor { get; }

        public ArmorItem(IItem item, AnimatorOverrideController animatorController, int armor) : this()
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;

            AnimatorController = animatorController ?? throw new ArgumentNullException(nameof(animatorController));
            Armor = armor.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}