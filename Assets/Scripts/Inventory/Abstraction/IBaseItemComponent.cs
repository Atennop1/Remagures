using UnityEngine;

namespace Remagures.Inventory
{
    public interface IBaseItemComponent
    {
        public string ItemName { get; }
        public string ItemDescription { get; }
        public Sprite ItemSprite { get; }
        public bool Stackable { get; }
    }
}
