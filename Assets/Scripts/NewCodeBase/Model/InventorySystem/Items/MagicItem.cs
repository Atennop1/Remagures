using System;
using Cysharp.Threading.Tasks;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class MagicItem : IMagicItem
    {
        public int UsingCooldownInMilliseconds { get; }

        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        
        public bool HasUsed { get; private set; }
        
        public MagicItem(IItem item, int usingCooldown)
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;
            
            UsingCooldownInMilliseconds = usingCooldown.ThrowExceptionIfLessOrEqualsZero();
        }
        
        public async void Use()
        {
            HasUsed = true;
            await UniTask.Yield();
            HasUsed = false;
        }
    }
}