using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public class NullItem : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
    }
}