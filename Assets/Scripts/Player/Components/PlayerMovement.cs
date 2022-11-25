using Cysharp.Threading.Tasks;
using Remagures.MapSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Remagures.Player.Components
{
    public class PlayerMovement : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private Player _player;

        [Space]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private MapExplorer _explorer;

        [Space]
        [SerializeField] private float _speed;
        private Rigidbody2D _thisRigidbody;
        
        public Vector2 PlayerViewDirection { get; private set; }
        public bool IsMoving { get; private set; }

        private const string MOVE_ANIMATOR_NAME = "moving";
        private PlayerAnimations _playerAnimations;
        private PlayerInteractingHandler _playerInteractingHandler;
        private PlayerAttacker _playerAttacker;

        public async void MoveTo(Vector3 to)
        {
            IsMoving = true;
            SetupMove((to - transform.position).normalized);

            while (transform.position != to)
            {
                if (Vector3.Distance(transform.position, to) < 0.1f)
                {
                    transform.position = to;
                    break;
                }
                
                await UniTask.WaitForFixedUpdate();
            }

            IsMoving = false;
        }
        
        private async void MoveInDirection(Vector2 input)
        {
            IsMoving = true;
            var joyStickCoefficient = input.y / input.x;
        
            if (joyStickCoefficient is >= 1 or <= -1) PlayerViewDirection = input.y > 0 ? Vector2.up : Vector2.down;
            else PlayerViewDirection = input.x > 0 ? Vector2.right : Vector2.left;

            SetupMove(PlayerViewDirection);
            while (_playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero)
                await UniTask.WaitForFixedUpdate();

            IsMoving = false;
        }

        private void SetupMove(Vector3 direction)
        {
            _player.ChangeState(PlayerState.Walk);
            _playerAnimations.ChangeAnim(MOVE_ANIMATOR_NAME, true);
            
            _thisRigidbody.velocity = direction * (_speed * UnityEngine.Time.deltaTime);
            _playerAnimations.SetAnimFloat(direction);
            _explorer.Explore();
        }

        private void FixedUpdate()
        {
            if (_playerInteractingHandler.CurrentState == InteractingState.Interact)
            {
                _playerInput.enabled = false;
                _thisRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                _playerInput.enabled = true;
                _thisRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (_playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero && _playerAttacker.CanAttack &&
                _player.CurrentState is PlayerState.Walk or PlayerState.Idle &&
                _playerInteractingHandler.CurrentState != InteractingState.Interact)
            {
                MoveInDirection(_playerInput.actions["Move"].ReadValue<Vector2>());
                return;
            }
            
            if (IsMoving) 
                return;

            _player.ChangeState(PlayerState.Idle);
            _thisRigidbody.velocity = Vector2.zero;
            _playerAnimations.ChangeAnim(MOVE_ANIMATOR_NAME, false);
        }

        private void Awake()
        {
            _thisRigidbody = GetComponent<Rigidbody2D>();
            _playerAnimations = _player.GetPlayerComponent<PlayerAnimations>();
            _playerAttacker = _player.GetPlayerComponent<PlayerAttacker>();
            _playerInteractingHandler = _player.GetPlayerComponent<PlayerInteractingHandler>();
        }
    }
}