using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.Character
{
    public sealed class CharacterMagicApplier
    {
        public bool IsApplying { get; private set; }
        public bool CanApply => _canActivate && !IsApplying && _magicInventorySelector.SelectedCell.Item.Magic.CanActivate();
        
        private readonly IInventoryCellSelector<IMagicItem> _magicInventorySelector;
        private bool _canActivate;

        public CharacterMagicApplier(IInventoryCellSelector<IMagicItem> magicInventorySelector) 
            => _magicInventorySelector = magicInventorySelector ?? throw new ArgumentNullException(nameof(magicInventorySelector));

        public async void Apply()
        {
            if (!CanApply) 
                return;
            
            _magicInventorySelector.SelectedCell.Item.Magic.Activate();
            
            await ActivateUsingCooldown();
            await ActivateApplyingCooldown();
        }

        private async UniTask ActivateApplyingCooldown()
        {
            IsApplying = false;
            await UniTask.Delay(_magicInventorySelector.SelectedCell.Item.Magic.Data.ApplyingTimeInMilliseconds);
            IsApplying = true;
        }

        private async UniTask ActivateUsingCooldown()
        {
            _canActivate = false;
            await UniTask.Delay(_magicInventorySelector.SelectedCell.Item.Magic.Data.CooldownInMilliseconds);
            _canActivate = true;
        }
    }
}