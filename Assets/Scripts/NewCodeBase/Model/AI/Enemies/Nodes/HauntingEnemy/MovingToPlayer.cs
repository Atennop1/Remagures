using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies.HauntingEnemy
{
    public sealed class MovingToPlayer : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyAnimations SharedEnemyAnimations;
        public SharedEnemyTargetData SharedEnemyTargetData;
        
        public override TaskStatus OnUpdate()
        {
            SharedEnemyMovement.Value.Move(SharedEnemyTargetData.Value.Transform.position);
            SharedEnemyAnimations.Value.SetIsWakeUp(true);
            return TaskStatus.Running;
        }
    }
}