using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.UpgradeSystem
{
    public readonly struct UpgradeArmorItemData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly Sprite Sprite;
        public readonly bool IsStackable;

        public readonly AnimatorOverrideController AnimatorController;
        public readonly int Armor;

        public UpgradeArmorItemData(UpgradeItemData upgradeItemData, AnimatorOverrideController animatorController, int armor)
        {
            Name = upgradeItemData.Name;
            Description = upgradeItemData.Description;
            Sprite = upgradeItemData.Sprite;
            IsStackable = upgradeItemData.IsStackable;
            
            AnimatorController = animatorController ?? throw new ArgumentNullException(nameof(animatorController));
            Armor = armor.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}