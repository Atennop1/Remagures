using System;
using UnityEngine;

namespace Remagures.Model.UpgradeSystem
{
    public readonly struct UpgradeItemData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly Sprite Sprite;
        public readonly bool IsStackable;

        public UpgradeItemData(string name, string description, Sprite sprite, bool isStackable)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
            IsStackable = isStackable;
        }
    }
}