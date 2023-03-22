using System;
using Remagures.Model.Magic;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.UpgradeSystem
{
    public readonly struct UpgradeMagicItemData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly Sprite Sprite;
        public readonly bool IsStackable;

        public readonly IMagic Magic;
        public readonly int UsingCooldownInMilliseconds;
        
        public UpgradeMagicItemData(UpgradeItemData upgradeItemData, IMagic magic, int usingCooldownInMilliseconds)
        {
            Name = upgradeItemData.Name;
            Description = upgradeItemData.Description;
            Sprite = upgradeItemData.Sprite;
            IsStackable = upgradeItemData.IsStackable;

            Magic = magic ?? throw new ArgumentNullException(nameof(magic));
            UsingCooldownInMilliseconds = usingCooldownInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}