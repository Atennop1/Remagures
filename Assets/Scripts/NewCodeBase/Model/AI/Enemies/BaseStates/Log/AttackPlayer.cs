using System;
using Remagures.View.Enemies;
using UnityEngine;

namespace Remagures.Model.AI.Enemies 
{
    public sealed class AttackPlayer : IState
    {
        private readonly IEnemyMovement _enemyMovement;
        private readonly EnemyTargetData _enemyTargetData;
        private readonly IEnemyMovementView _enemyMovementView;

        public AttackPlayer(IEnemyMovement enemyMovement, EnemyTargetData enemyTargetData, IEnemyMovementView enemyMovementView)
        {
            _enemyMovement = enemyMovement ?? throw new ArgumentNullException(nameof(enemyMovement));
            _enemyTargetData = enemyTargetData;
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            var enemyPosition = _enemyMovement.Transform;
            var totalMovingPoint = Vector3.MoveTowards(enemyPosition.position, _enemyTargetData.Transform.position, 1);
            _enemyMovementView.SetAnimationsVector(totalMovingPoint - enemyPosition.position);
        }

        public void OnEnter()
        {
            _enemyMovement.StopMoving();
            _enemyMovementView.SetIsStaying(true);
        }

        public void OnExit() { }
    }
}