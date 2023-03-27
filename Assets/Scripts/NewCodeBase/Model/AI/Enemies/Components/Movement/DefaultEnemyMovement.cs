using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Remagures.Tools;
using Remagures.View.Enemies;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class DefaultEnemyMovement : IEnemyMovement
    {
        public bool CanMove { get; private set; }
        public Transform Transform => _rigidbody.transform;
        
        private readonly Rigidbody2D _rigidbody;
        private readonly IEnemyMovementView _enemyMovementView;
        private readonly float _speed;

        private CancellationTokenSource _cancellationTokenSource;

        public DefaultEnemyMovement(Rigidbody2D rigidbody, IEnemyMovementView enemyMovementView, float speed)
        {
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
            _speed = speed.ThrowExceptionIfLessOrEqualsZero();
        }

        public async void Move(Vector3 targetPosition)
        {
            if (!CanMove) 
                return;
            
            StopMoving();
            _cancellationTokenSource = new CancellationTokenSource();
            
            await MoveTask(targetPosition);
            _enemyMovementView.SetIsStaying(false);
            _enemyMovementView.SetAnimationsVector((Vector2)targetPosition - _rigidbody.position);
        }

        public void StopMoving()
        {
            _cancellationTokenSource.Cancel();
            _enemyMovementView.SetIsStaying(true);
            CanMove = true;
        }
        
        private async UniTask MoveTask(Vector3 targetPosition)
        {
            CanMove = false;
            
            while (Vector3.Distance(_rigidbody.transform.position, targetPosition) > 0.05f)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                    return;

                var direction = (targetPosition - _rigidbody.transform.position).normalized;
                var totalPosition = _rigidbody.transform.position + _speed * Time.deltaTime * direction;
                
                _rigidbody.MovePosition(totalPosition);
                await UniTask.WaitForFixedUpdate();
            }
            
            _rigidbody.transform.position = targetPosition;
            CanMove = true;
        }
    }
}