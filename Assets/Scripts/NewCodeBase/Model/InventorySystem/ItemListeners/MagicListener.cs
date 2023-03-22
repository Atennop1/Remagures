using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.Magic;
using Remagures.Root;

namespace Remagures.Model.InventorySystem
{
    public class MagicListener : IUpdatable //TODO make this and other listeners as decorators
    {
        private readonly IMagicItem _magicItem;
        private readonly IMagicApplier _magicApplier;
        private bool _canUse;

        public MagicListener(IMagicItem magicItem, IMagicApplier magicApplier)
        {
            _magicItem = magicItem ?? throw new ArgumentNullException(nameof(magicItem));
            _magicApplier = magicApplier ?? throw new ArgumentNullException(nameof(magicApplier));
        }

        public async void Update()
        {
            if (!_magicItem.HasUsed || !_canUse) 
                return;
            
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