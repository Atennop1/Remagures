using System;
using Remagures.MapSystem;
using Remagures.Timeline;
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

        public void Move(Vector2 input)
        {
            IsMoving = true;
            var joyStickCoefficient = input.y / input.x;
        
            if (joyStickCoefficient is >= 1 or <= -1) PlayerViewDirection = input.y > 0 ? Vector2.up : Vector2.down;
            else PlayerViewDirection = input.x > 0 ? Vector2.right : Vector2.left;
        
            _thisRigidbody.velocity = PlayerViewDirection * (_speed * UnityEngine.Time.deltaTime);
            _playerAnimations.SetAnimFloat(PlayerViewDirection);
            _explorer.Explore();
            IsMoving = false;
        }
        
        private void Start() 
        {
            _thisRigidbody = GetComponent<Rigidbody2D>();
        }
    
        private void SetDirection()
        {
            var input = _playerInput.actions["Move"].ReadValue<Vector2>();
            Move(input);
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

            if (_playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero && _playerAttacker.CanAttack && _player.CurrentState is PlayerState.Walk or PlayerState.Idle && 
                _playerInteractingHandler.CurrentState != InteractingState.Interact &&
                !(TimelineView.Instance != null && TimelineView.Instance.IsPlaying))
            {
                SetDirection();
                _player.ChangeState(PlayerState.Walk);
                _playerAnimations.ChangeAnim(MOVE_ANIMATOR_NAME, true);
            }
            else if (_player.CurrentState != PlayerState.Stagger)
            {
                _player.ChangeState(PlayerState.Idle);
                _thisRigidbody.velocity = Vector2.zero;
                _playerAnimations.ChangeAnim(MOVE_ANIMATOR_NAME, false);
            }
        }

        private void Awake()
        {
            _playerAnimations = _player.GetPlayerComponent<PlayerAnimations>();
            _playerAttacker = _player.GetPlayerComponent<PlayerAttacker>();
            _playerInteractingHandler = _player.GetPlayerComponent<PlayerInteractingHandler>();
        }
    }
}