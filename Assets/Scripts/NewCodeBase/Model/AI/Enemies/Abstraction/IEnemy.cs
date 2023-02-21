using Remagures.Model.Health;

namespace Remagures.Model.AI.Enemies
{
    public interface IEnemy
    {
        IEnemyMovement Movement { get; }
        IHealth Health { get; }
        
        IEnemyAnimations Animations { get; }
        StateMachine StateMachine { get; }
    }
}