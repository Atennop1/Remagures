using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.Character
{
    public sealed class CharacterAttacker
    {
        public bool IsAttacking { get; private set; }
        public bool CanAttack => _hasCooldownActive && !IsAttacking;
        
        private readonly IInventoryCellSelector<IWeaponItem> _weaponInventorySelector;
        private bool _hasCooldownActive;

        public CharacterAttacker(IInventoryCellSelector<IWeaponItem> weaponInventorySelector) 
            => _weaponInventorySelector = weaponInventorySelector ?? throw new ArgumentNullException(nameof(weaponInventorySelector));

        public async void Attack()
        {
            if (!CanAttack) 
                return;
            
            _weaponInventorySelector.SelectedCell.Item.Attack.Use();
            
            await ActivateUsingCooldown();
            await ActivateAttackingCooldown();
        }

        private async UniTask ActivateAttackingCooldown()
        {
            IsAttacking = false;
            await UniTask.Delay(_weaponInventorySelector.SelectedCell.Item.Attack.Data.AttackingTimeInMilliseconds);
            IsAttacking = true;
        }

        private async UniTask ActivateUsingCooldown()
        {
            _hasCooldownActive = false;
            await UniTask.Delay(_weaponInventorySelector.SelectedCell.Item.Attack.Data.UsingCooldownInMilliseconds);
            _hasCooldownActive = true;
        }
    }
}
