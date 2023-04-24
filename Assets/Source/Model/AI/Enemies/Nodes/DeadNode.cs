using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies
{
    public sealed class DeadNode : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyAnimations SharedEnemyAnimations;
        
        public override void OnAwake()
        {
            SharedEnemyMovement.Value.StopMoving();
            SharedEnemyAnimations.Value.SetIsDead(true);
        }
    }
}