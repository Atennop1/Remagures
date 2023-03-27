using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies.MeleeEnemy
{
    public sealed class AttackingPlayer : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyAttacker SharedEnemyAttacker;
        
        public override void OnAwake()
        {
            SharedEnemyMovement.Value.StopMoving();
            SharedEnemyAttacker.Value.Attack();
        }
    }
}