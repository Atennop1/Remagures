using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class UsableItem : IUsableItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }

        public UsableItem(IItem item)
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;
        }
        
        public void Use() { }
    }
}
