using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Remagures.Model.Input
{
    public class MovementInput : IMovementInput
    {
        private readonly PlayerInput _unityInput;

        public MovementInput(PlayerInput unityInput)
            => _unityInput = unityInput ?? throw new ArgumentNullException(nameof(unityInput));

        public Vector2 MoveDirection 
            => _unityInput.actions["Move"].ReadValue<Vector2>();
        
        public Vector2 CharacterLookDirection
        {
            get
            {
                var joyStickCoefficient = MoveDirection.y / MoveDirection.x;

                if (joyStickCoefficient is >= 1 or <= -1) 
                    return MoveDirection.y > 0 ? Vector2.up : Vector2.down;
                
                return MoveDirection.x > 0 ? Vector2.right : Vector2.left;
            }
        }
    }
}