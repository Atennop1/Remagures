using BehaviorDesigner.Runtime.Tasks;

namespace Remagures.Model.AI.Enemies.TurretEnemy
{
    public sealed class Staying : Action
    {
        public SharedEnemyAnimations SharedEnemyAnimations;
        
        public override void OnAwake()
        {
            SharedEnemyAnimations.Value.SetIsWakeUp(false);
            SharedEnemyAnimations.Value.SetIsStaying(true);
        }
    }
}