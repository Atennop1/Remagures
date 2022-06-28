using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed;

    private Rigidbody2D _thisRigidbody;
    private Vector2 _moveVector;

    private void Start() 
    {
        _thisRigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void SetDirection()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        Move(input);
    }

    public void Move(Vector2 input)
    {   
        float joyStickCoefficent = input.y / input.x;
        if (joyStickCoefficent >= 1 || joyStickCoefficent <= -1)
        {
            if (input.y > 0) _moveVector = Vector2.up;
            else _moveVector = Vector2.down;
        }
        else
        {
            if (input.x > 0) _moveVector = Vector2.right;
            else _moveVector = Vector2.left;
        }
        _thisRigidbody.velocity = _moveVector * _speed * Time.deltaTime;
        _player.PlayerAnimations.SetAnimFloat(_moveVector);
    }

    private void FixedUpdate()
    {
        if (_player.PlayerInteract.CurrentState == InteractingState.Interact)
        {
            _playerInput.enabled = false;
            _thisRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            _playerInput.enabled = true;
            _thisRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (_playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero && _player.PlayerAttack.CanAttack && (_player.CurrentState == PlayerState.Walk || _player.CurrentState == PlayerState.Idle) && _player.PlayerInteract.CurrentState != InteractingState.Interact)
        {
            SetDirection();
            _player.CurrentState = PlayerState.Walk;
            _player.PlayerAnimations.ChangeAnim("moving", true);
        }
        else if (_player.CurrentState != PlayerState.Stagger)
        {
            _player.CurrentState = PlayerState.Idle;
            _thisRigidbody.velocity = Vector2.zero;
            _player.PlayerAnimations.ChangeAnim("moving", false);
        }
    }
}
