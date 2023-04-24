using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Remagures.Model.AI.Enemies.PatrollingEnemy
{
    public sealed class Patrolling : Action
    {
        public SharedPatrolEnemyMovement SharedPatrolEnemyMovement;
        public SharedEnemyAnimations SharedEnemyAnimations;
        
        public override TaskStatus OnUpdate()
        {
            SharedPatrolEnemyMovement.Value.Move(SharedPatrolEnemyMovement.Value.CurrentPointTransform.position);
            return TaskStatus.Running;
        }

        public void OnEnter()
            => SharedEnemyAnimations.Value.SetIsWakeUp(true);
    }
}