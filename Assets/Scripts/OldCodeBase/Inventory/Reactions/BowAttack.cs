using System.Collections;
using Remagures.Components;
using Remagures.Model.Character;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Inventory
{
    public class BowAttack : MonoBehaviour
    {
        [SerializeField] private FloatValue _currentMagic;
        [SerializeField] private Signal _decreaseMagicSignal;
        [SerializeField] private GameObject _projectile;
        private Player _player;
    
        private readonly int ATTACKING_ANIMATOR_NAME = Animator.StringToHash("attacking");
        private readonly int MOVE_X_ANIMATOR_NAME = Animator.StringToHash("moveX");
        private readonly int MOVE_Y_ANIMATOR_NAME = Animator.StringToHash("moveY");

        private PlayerAttacker _playerAttacker;
        private PlayerInteractingHandler _playerInteractingHandler;
        private Animator _playerAnimator;

        public void MagicAttack()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            _playerAttacker = _player.GetPlayerComponent<PlayerAttacker>();
            _playerInteractingHandler = _player.GetPlayerComponent<PlayerInteractingHandler>();
            _playerAnimator = _player.GetPlayerComponent<PlayerAnimations>().PlayerAnimator;
            
            if (_playerAttacker.CanAttack && _player.CurrentState != PlayerState.Attack && _playerInteractingHandler.CurrentState != InteractingState.Interact)
                _playerAttacker.StartMagicAttackCoroutine(MagicAttackCoroutine());
        }

        private IEnumerator MagicAttackCoroutine()
        {
            if (_playerAnimator.GetBool(ATTACKING_ANIMATOR_NAME) || !CheckArrowValid()) yield break;
            _player.ChangeState(PlayerState.Attack);
            
            yield return null;
            MakeArrow();
            yield return new WaitForSeconds(_player.PlayerData.CharacterInfo.BowReloadTime);

            if (_player.CurrentState != PlayerState.Interact)
                _player.ChangeState(PlayerState.Idle);

            _playerAttacker.SetAttackCoroutineToNull();
        }

        private void MakeArrow()
        {
            if (!(_currentMagic.Value >= _player.PlayerData.MagicCounter.MagicCost)) return;
        
            _decreaseMagicSignal.Invoke();
            var throwVector = new Vector2(_playerAnimator.GetFloat(MOVE_X_ANIMATOR_NAME), _playerAnimator.GetFloat(MOVE_Y_ANIMATOR_NAME));
            
            var arrow = Instantiate(_projectile, _player.gameObject.transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.GetComponent<CharacterAttack>().Init(_player.PlayerData.UniqueSetup);

            var arrowDirection = ChooseArrowDirection();
            arrow.Setup(throwVector, arrowDirection);
            CorrectArrowPosition(arrow.gameObject, arrowDirection);
        }

        private void CorrectArrowPosition(GameObject arrow, Vector3 arrowDirection)
        {
            switch (arrowDirection.z)
            {
                case 0:
                    arrow.transform.position += Vector3.up * 0.5f;
                    break;
                case 180:
                    arrow.transform.position += Vector3.up * 0.5f;
                    break;
                case 90:
                    arrow.transform.position += Vector3.up;
                    break;
            }
        }
        
        private bool CheckArrowValid()
        {
            if (!_projectile.TryGetComponent(out Arrow _)) return false;
        
            var hit = Physics2D.Raycast(_player.transform.position, new Vector2(
                _playerAnimator.GetFloat(MOVE_X_ANIMATOR_NAME), 
                _playerAnimator.GetFloat(MOVE_Y_ANIMATOR_NAME)), 1, 3);
        
            return hit.collider == null;
        }
    
        private Vector3 ChooseArrowDirection()
            => new(0, 0, Mathf.Atan2(_playerAnimator.GetFloat(MOVE_Y_ANIMATOR_NAME), 
                _playerAnimator.GetFloat(MOVE_X_ANIMATOR_NAME)) * Mathf.Rad2Deg);
    }
}
