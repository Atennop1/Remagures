using Cysharp.Threading.Tasks;
using Remagures.Inventory;
using Remagures.View.Character;

namespace Remagures.Model.Character
{
    public class CharacterAttacker
    {
        public bool CanAttack 
            => _attackTask.Status == UniTaskStatus.Succeeded;

        //private PlayerInteractingHandler _playerInteractingHandler;
        
        private readonly ICharacterAttackerView _view;
        private UniTask _attackTask;
        
        private Player _player;
        private UniqueSetup _uniqueSetup;

        public void UseAttack()
        {
            //if (_playerInteractingHandler.CurrentState == InteractingState.Ready) //TODO place this logic in another class
            //{
            //    _playerInteractingHandler.Interact();
            //    return;
            //}
            
            if (_attackTask.Status == UniTaskStatus.Succeeded && _player.CurrentState != PlayerState.Attack && _uniqueSetup.WeaponSlot.ThisCell.Item.ItemName != "")
                _attackTask = Attack();
        }

        private async UniTask Attack()
        {
            _player.ChangeState(PlayerState.Attack);
            _view.PlayAttackAnimation();

            await UniTask.Delay(330);
            _view.StopAttackAnimation();

            if (_player.CurrentState != PlayerState.Interact)
                _player.ChangeState(PlayerState.Idle);
        }
    
        public void UseMagicAttack()
            => (_uniqueSetup.MagicSlot.ThisCell.Item as IMagicItem)?.UsingEvent?.Invoke();
    }
}
