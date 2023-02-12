using Remagures.RuneSystem;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public readonly struct RuneItem : IRuneItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        
        public RuneType Type { get; }

        public RuneItem(IItem item, RuneType type)
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;
            
            Type = type;
        }
    }
}
