using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.Magic;
using Remagures.Root;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class ItemWithMagicUsing : IMagicItem
    {
        public string Name => _magicItem.Name;
        public string Description => _magicItem.Description;
        public Sprite Sprite => _magicItem.Sprite;
        public bool IsStackable => _magicItem.IsStackable;

        public int UsingCooldownInMilliseconds => _magicItem.UsingCooldownInMilliseconds;
        public IMagic Magic => _magicItem.Magic;
        
        private readonly IMagicItem _magicItem;
        private readonly IMagicApplier _magicApplier;
        private bool _canUse;

        public ItemWithMagicUsing(IMagicItem magicItem, IMagicApplier magicApplier)
        {
            _magicItem = magicItem ?? throw new ArgumentNullException(nameof(magicItem));
            _magicApplier = magicApplier ?? throw new ArgumentNullException(nameof(magicApplier));
        }

        public async void Use()
        {
            if (!_canUse) 
                return;
            
            _magicItem.Use();
            _magicApplier.Use(_magicItem.Magic);
            await StartCooldown();
        }

        private async UniTask StartCooldown()
        {
            _canUse = false;
            await UniTask.Delay(_magicItem.UsingCooldownInMilliseconds);
            _canUse = true;
        }
    }
}