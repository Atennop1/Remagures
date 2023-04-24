using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies.TurretEnemy
{
    public sealed class Shooting : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyAnimations SharedEnemyAnimations;
        public SharedEnemyTargetData SharedEnemyTargetData;
        public SharedEnemyDirectionAttacker SharedEnemyAttacker;
        
        public override TaskStatus OnUpdate()
        {
            var direction = SharedEnemyTargetData.Value.Transform.transform.position - SharedEnemyMovement.Value.Transform.position;
            SharedEnemyAttacker.Value.Attack(direction);
            
            SharedEnemyAnimations.Value.SetAnimationsVector(direction);
            return TaskStatus.Running;
        }

        public override void OnAwake()
        {
            SharedEnemyAnimations.Value.SetIsWakeUp(true);
            SharedEnemyAnimations.Value.SetIsStaying(true);
        }
    }
}