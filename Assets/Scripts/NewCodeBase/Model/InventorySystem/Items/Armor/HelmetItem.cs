using System;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public readonly struct HelmetItem : IHelmetItem
    {
        public string Name => _armorItem.Name;
        public string Description => _armorItem.Description;

        public Sprite Sprite => _armorItem.Sprite;
        public bool IsStackable => _armorItem.IsStackable;

        public AnimatorOverrideController AnimatorController => _armorItem.AnimatorController;
        public int Armor => _armorItem.Armor;
        
        private readonly IArmorItem _armorItem;

        public HelmetItem(IArmorItem armorItem)
            => _armorItem = armorItem ?? throw new ArgumentNullException(nameof(armorItem));
    }
}