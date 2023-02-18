using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.InventorySystem;
using Remagures.View.Character;

namespace Remagures.Model.Character
{
    public sealed class CharacterAttacker
    {
        private readonly ICharacterAttackerView _view;
        private readonly IInventoryCellSelector<IWeaponItem> _weaponInventorySelector;

        public bool IsAttacking { get; private set; }
        private const int ATTACKING_TIME_IN_MILLISECONDS = 330;

        public CharacterAttacker(ICharacterAttackerView view, IInventoryCellSelector<IWeaponItem> weaponInventorySelector)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _weaponInventorySelector = weaponInventorySelector ?? throw new ArgumentNullException(nameof(weaponInventorySelector));
        }

        public async void UseAttack()
        {
            //if (_playerInteractingHandler.CurrentState == InteractingState.Ready) //TODO place this logic in another class
            //{
            //    _playerInteractingHandler.Interact();
            //    return;
            //}

            if (IsAttacking) 
                return;
            
            _weaponInventorySelector.SelectedCell.Item.Use();
            await AttackTask();
        }

        private async UniTask AttackTask()
        {
            IsAttacking = true;
            _view.PlayAttackAnimation();
            
            await UniTask.Delay(ATTACKING_TIME_IN_MILLISECONDS);
            
            _view.StopAttackAnimation();
            IsAttacking = false;
        }
    }
}
