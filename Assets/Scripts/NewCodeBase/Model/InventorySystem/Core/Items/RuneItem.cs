using System;
using Remagures.Model.RuneSystem;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class RuneItem : IRuneItem
    {
        public IRune Rune { get; }
        
        public string Name => _item.Name;
        public string Description => _item.Description;
        public Sprite Sprite => _item.Sprite;
        public bool IsStackable => _item.IsStackable;

        private readonly IItem _item;

        public RuneItem(IItem item, IRune rune)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            Rune = rune ?? throw new ArgumentNullException(nameof(rune));
        }
    }
}
