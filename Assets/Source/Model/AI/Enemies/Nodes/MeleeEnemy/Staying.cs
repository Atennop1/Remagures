using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies.MeleeEnemy
{
    public sealed class Staying : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        
        public override void OnAwake() 
            => SharedEnemyMovement.Value.StopMoving();
    }
}