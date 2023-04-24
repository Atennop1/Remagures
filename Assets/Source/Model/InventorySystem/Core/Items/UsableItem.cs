using System;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class UsableItem : IUsableItem
    {
        public string Name => _item.Name;
        public string Description => _item.Description;
        public Sprite Sprite => _item.Sprite;
        public bool IsStackable => _item.IsStackable;

        private readonly IItem _item;

        public UsableItem(IItem item)
            => _item = item ?? throw new ArgumentNullException(nameof(item));
        
        public void Use() { }
    }
}
