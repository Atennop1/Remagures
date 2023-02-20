using System;
using Remagures.Model.AI.Pathfinding;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public class PatrolEnemyMovement : IEnemyMovement
    {
        public bool CanMove => _enemyMovement.CanMove;
        public Transform Transform => _enemyMovement.Transform;
        public Transform CurrentPointTransform => _patrolPoints[_currentPointIndex];
        
        private readonly IEnemyMovement _enemyMovement;
        private readonly Pathfinder _pathfinder;
        private readonly Transform[] _patrolPoints;
        
        private int _currentPointIndex;

        public PatrolEnemyMovement(IEnemyMovement enemyMovement, Pathfinder pathfinder, Transform[] patrolPoints)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _pathfinder = pathfinder ?? throw new ArgumentNullException(nameof(pathfinder));
            _patrolPoints = patrolPoints ?? throw new ArgumentNullException(nameof(patrolPoints));
        }

        public void Move(Vector3 targetPosition)
        {
            if (!CanMove)
                return;

            _enemyMovement.Move(targetPosition);
            
            if (targetPosition == CurrentPointTransform.position && _pathfinder.FindPath(Transform.position, targetPosition).Count is 0)
                ChangeGoal();
        }

        public void StopMoving()
            => _enemyMovement.StopMoving();

        private void ChangeGoal()
        {
            if (_currentPointIndex == _patrolPoints.Length - 1)
            {
                _currentPointIndex = 0;
            }
            else
            {
                _currentPointIndex++;
            }
        }
    }
}