using System.Collections;
using Remagures.Inventory;
using UnityEngine;

namespace Remagures.Model.Character
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private UniqueSetup _uniqueSetup;
        
        public bool CanAttack => _attackCoroutine == null;
        private Coroutine _attackCoroutine;

        private const string ATTACK_ANIMATOR_NAME = "attacking";
        private PlayerInteractingHandler _playerInteractingHandler;
        private PlayerAnimations _playerAnimations;

        public void AttackMethod()
        {
            if (_playerInteractingHandler.CurrentState == InteractingState.Ready)
            {
                _playerInteractingHandler.Interact();
                return;
            }
            
            if (_attackCoroutine == null && _player.CurrentState != PlayerState.Attack && _uniqueSetup.WeaponSlot.ThisCell.Item.ItemName != "")
                _attackCoroutine = StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            _player.ChangeState(PlayerState.Attack);

            _playerAnimations.ChangeAnim(ATTACK_ANIMATOR_NAME, true);
            yield return null;

            _playerAnimations.ChangeAnim(ATTACK_ANIMATOR_NAME, false);
            yield return new WaitForSeconds(0.33f);

            if (_player.CurrentState != PlayerState.Interact)
                _player.ChangeState(PlayerState.Idle);

            _attackCoroutine = null;
        }
    
        public void MagicAttackMethod()
            => (_uniqueSetup.MagicSlot.ThisCell.Item as IMagicItem)?.UsingEvent?.Invoke();

        public void StartMagicAttackCoroutine(IEnumerator method)
        {
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
            _attackCoroutine = StartCoroutine(method);
        }

        public void SetAttackCoroutineToNull()
            => _attackCoroutine = null;

        private void Awake()
        {
            _playerInteractingHandler = _player.GetPlayerComponent<PlayerInteractingHandler>();
            _playerAnimations = _player.GetPlayerComponent<PlayerAnimations>();
        }
    }
}
