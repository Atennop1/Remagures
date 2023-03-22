using System;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class Item : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get;}
        public bool IsStackable { get; }

        public Item(string name, string description, Sprite sprite, bool isStackable)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
            IsStackable = isStackable;
        }
    }
}
