using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies.HauntingEnemy
{
    public sealed class Sleeping : Action
    {
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedEnemyAnimations SharedEnemyAnimations;
        
        public override void OnAwake()
        {
            SharedEnemyMovement.Value.StopMoving();
            SharedEnemyAnimations.Value.SetIsWakeUp(false);
        }
    }
}