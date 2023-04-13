using System;
using Remagures.Model.Magic;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class MagicItem : IMagicItem
    {
        public IMagic Magic { get; }

        public string Name => _item.Name;
        public string Description => _item.Description;
        public Sprite Sprite => _item.Sprite;
        public bool IsStackable => _item.IsStackable;

        private readonly IItem _item;

        public MagicItem(IItem item, IMagic magic)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            Magic = magic ?? throw new ArgumentNullException(nameof(magic));
        }
    }
}