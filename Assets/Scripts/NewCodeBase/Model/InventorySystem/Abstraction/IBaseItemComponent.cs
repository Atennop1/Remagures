using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public interface IBaseItemComponent
    {
        public string ItemName { get; }
        public string ItemDescription { get; }
        public Sprite ItemSprite { get; }
        public bool Stackable { get; }
    }
}
