using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class UsableItem : IUsableItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        
        public bool HasUsed { get; private set; }
        
        public UsableItem(IItem item)
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;
        }
        
        public async void Use()
        {
            HasUsed = true;
            await UniTask.Yield();
            HasUsed = false;
        }
    }
}
