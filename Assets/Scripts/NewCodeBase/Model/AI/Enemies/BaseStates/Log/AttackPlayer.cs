using System;
using Remagures.View.Enemies;
using UnityEngine;

namespace Remagures.Model.AI.Enemies 
{
    public sealed class AttackPlayer : IState
    {
        private readonly IEnemyWithTarget _enemyWithTarget;
        private readonly IEnemyMovementView _enemyMovementView;

        public AttackPlayer(IEnemyWithTarget enemyWithTarget, IEnemyMovementView enemyMovementView)
        {
            _enemyWithTarget = enemyWithTarget ?? throw new ArgumentNullException(nameof(enemyWithTarget));
            _enemyMovementView = enemyMovementView ?? throw new ArgumentNullException(nameof(enemyMovementView));
        }

        public void Update()
        {
            var enemyAnimator = _enemyWithTarget.Animations.Animator;
            var totalMovingPoint = Vector3.MoveTowards(enemyAnimator.transform.position, _enemyWithTarget.TargetData.Transform.position, 1);
            _enemyWithTarget.Animations.SetAnimationsVector(totalMovingPoint - enemyAnimator.transform.position);
        }

        public void OnEnter()
        {
            _enemyWithTarget.Movement.StopMoving();
            _enemyMovementView.SetIsStaying(true);
        }

        public void OnExit() { }
    }
}