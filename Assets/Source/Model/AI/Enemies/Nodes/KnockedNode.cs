using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies
{
    public sealed class KnockedNode : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyAnimations SharedEnemyAnimations;
        
        public override void OnAwake()
        {
            SharedEnemyMovement.Value.StopMoving();
            SharedEnemyAnimations.Value.SetIsKnocked(true);
        }
    }
}