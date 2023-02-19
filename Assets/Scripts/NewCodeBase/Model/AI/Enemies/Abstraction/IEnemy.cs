using SM = Remagures.Model.AI.StateMachine.StateMachine;

namespace Remagures.Model.AI.Enemies
{
    public interface IEnemy
    {
        IEnemyMovement Movement { get; }
        Health.Health Health { get; }
        
        EnemyAnimations Animations { get; }
        SM StateMachine { get; }
    }
}