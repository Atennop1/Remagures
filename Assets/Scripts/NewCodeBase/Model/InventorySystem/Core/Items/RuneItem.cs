using System;
using Remagures.Model.RuneSystem;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class RuneItem : IRuneItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        
        public IRune Rune { get; }

        public RuneItem(IItem item, IRune rune)
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;

            Rune = rune ?? throw new ArgumentNullException(nameof(rune));
        }
    }
}
