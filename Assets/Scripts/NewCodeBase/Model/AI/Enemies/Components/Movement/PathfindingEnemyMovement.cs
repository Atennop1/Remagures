using System;
using Cysharp.Threading.Tasks;
using Remagures.Model.AI.Pathfinding;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public class PathfindingEnemyMovement : IEnemyMovement
    {
        public bool CanMove { get; private set; }
        public Transform Transform => _enemyMovement.Transform;
        
        private readonly IEnemyMovement _enemyMovement;
        private readonly Pathfinder _pathfinder;

        public PathfindingEnemyMovement(IEnemyMovement enemyMovement, Pathfinder pathfinder)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _pathfinder = pathfinder ?? throw new ArgumentNullException(nameof(pathfinder));
            
            CanMoveResettingTask();
            CanMove = true;
        }
        
        public void Move(Vector3 targetPosition)
        {
            if (!CanMove) 
                return;
            
            var path = _pathfinder.FindPath(Transform.position, targetPosition);

            if (path.Count <= 0)
                return;

            targetPosition = path[0].WorldPosition + new Vector3(0, 0.5f, 0);
            _enemyMovement.Move(targetPosition);
        }

        public void StopMoving()
            => _enemyMovement.StopMoving();

        private async void CanMoveResettingTask()
        {
            while (true)
            {
                await UniTask.Delay(1000);
                CanMove = true;
            }
        }
    }
}