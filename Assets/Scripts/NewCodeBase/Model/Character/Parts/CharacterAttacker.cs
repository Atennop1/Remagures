using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.Character
{
    public sealed class CharacterAttacker
    {
        public bool IsAttacking { get; private set; }
        
        private readonly IInventoryCellSelector<IWeaponItem> _weaponInventorySelector;
        private bool _canAttack;

        public CharacterAttacker(IInventoryCellSelector<IWeaponItem> weaponInventorySelector) 
            => _weaponInventorySelector = weaponInventorySelector ?? throw new ArgumentNullException(nameof(weaponInventorySelector));

        public async void UseAttack()
        {
            //if (_playerInteractingHandler.CurrentState == InteractingState.Ready) //TODO place this logic in another class
            //{
            //    _playerInteractingHandler.Interact();
            //    return;
            //}

            if (!_canAttack || IsAttacking) 
                return;
            
            _weaponInventorySelector.SelectedCell.Item.Use();
            
            await CanAttackChanging();
            await IsAttackingChanging();
        }

        private async UniTask IsAttackingChanging()
        {
            IsAttacking = false;
            await UniTask.Delay(_weaponInventorySelector.SelectedCell.Item.AttackingTimeInMilliseconds);
            IsAttacking = true;
        }

        private async UniTask CanAttackChanging()
        {
            _canAttack = false;
            await UniTask.Delay(_weaponInventorySelector.SelectedCell.Item.UsingCooldownInMilliseconds);
            _canAttack = true;
        }
    }
}
