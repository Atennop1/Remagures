using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.AI.Enemies.HauntingEnemy
{
    public sealed class AttackingPlayer : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyAnimations SharedEnemyAnimations;
        public SharedEnemyTargetData SharedEnemyTargetData;
        
        public override TaskStatus OnUpdate()
        {
            var enemyTransform = SharedEnemyMovement.Value.Transform;
            var finalMovementDirection = Vector3.MoveTowards(enemyTransform.position, SharedEnemyTargetData.Value.Transform.position, 1);
            SharedEnemyAnimations.Value.SetAnimationsVector(finalMovementDirection - enemyTransform.position);
            return TaskStatus.Running;
        }

        public override void OnAwake() 
            => SharedEnemyMovement.Value.StopMoving();
    }
}