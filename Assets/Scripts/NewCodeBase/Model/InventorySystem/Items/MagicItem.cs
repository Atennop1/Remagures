using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.Magic;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class MagicItem : IMagicItem
    {
        public int UsingCooldownInMilliseconds { get; }
        public IMagic Magic { get; }

        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        
        public bool HasUsed { get; private set; }
        
        public MagicItem(IItem item, IMagic magic, int usingCooldown)
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;

            Magic = magic ?? throw new ArgumentNullException(nameof(magic));
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