using System;
using Cysharp.Threading.Tasks;
using Remagures.Tools;
using Remagures.View.Character;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class CharacterMovement : ICharacterMovement
    {
        public Transform Transform => _rigidbody.transform;

        public Vector2 CharacterLookDirection { get; private set; }
        public bool IsMoving { get; private set; }
        
        private readonly ICharacterMovementView _view;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;

        public CharacterMovement(ICharacterMovementView view, Rigidbody2D rigidbody, float speed)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            _speed = speed.ThrowExceptionIfLessOrEqualsZero();

            var characterPositionStorage = new CharacterPositionStorage(new BinaryStorage());
            var characterPositionData = characterPositionStorage.Load();
            
            CharacterLookDirection = characterPositionData.CharacterLookDirection;
            Transform.position = characterPositionData.CharacterPosition - new Vector2(0, 0.6f);
            
            _view.DisplayCharacterLookDirection(CharacterLookDirection);
        }

        public void MoveTo(Vector3 endPosition)
        {
            IsMoving = true;
            var direction = (endPosition - Transform.position).normalized;
            
            CharacterLookDirection = direction;
            _rigidbody.velocity = direction * (_speed * UnityEngine.Time.deltaTime);
            
            _view.StartMoveAnimation();
            _view.DisplayCharacterLookDirection(direction);

            MovingTask(direction);
            IsMoving = false;
        }

        private async void MovingTask(Vector3 endPosition)
        {
            while (Transform.position != endPosition)
            {
                if (Vector3.Distance(Transform.position, endPosition) < 0.1f)
                {
                    Transform.position = endPosition;
                    break;
                }
                
                await UniTask.WaitForFixedUpdate();
            }
        }
    }
}