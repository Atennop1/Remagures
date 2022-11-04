using Remagures.AI.Enemies.Components;
using Remagures.Components.Base;
using SM = Remagures.AI.StateMachine;

namespace Remagures.AI.Enemies.Abstraction
{
    public interface IEnemy
    {
        IEnemyMovement Movement { get; }
        Health Health { get; }
        
        EnemyAnimations Animations { get; }
        SM.StateMachine StateMachine { get; }
    }
}