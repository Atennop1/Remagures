using Remagures.Components;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies
{
    public interface IEnemy
    {
        IEnemyMovement Movement { get; }
        Health Health { get; }
        
        EnemyAnimations Animations { get; }
        SM StateMachine { get; }
    }
}