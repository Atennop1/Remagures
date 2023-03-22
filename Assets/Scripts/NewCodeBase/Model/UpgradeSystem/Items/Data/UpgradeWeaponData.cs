using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.UpgradeSystem
{
    public readonly struct UpgradeWeaponData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly Sprite Sprite;
        public readonly bool IsStackable;

        public readonly AnimatorOverrideController AnimatorController;
        public readonly int UsingCooldownInMilliseconds;
        public readonly int Damage;

        public UpgradeWeaponData(UpgradeItemData upgradeItemData, AnimatorOverrideController animatorController, int usingCooldownInMilliseconds, int damage)
        {
            Name = upgradeItemData.Name;
            Description = upgradeItemData.Description;
            Sprite = upgradeItemData.Sprite;
            IsStackable = upgradeItemData.IsStackable;

            AnimatorController = animatorController ?? throw new ArgumentNullException(nameof(animatorController));
            UsingCooldownInMilliseconds = usingCooldownInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
            Damage = damage.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}