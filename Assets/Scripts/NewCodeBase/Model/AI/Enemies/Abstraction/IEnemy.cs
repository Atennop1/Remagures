using Remagures.Model.Health;

namespace Remagures.Model.AI.Enemies
{
    public interface IEnemy
    {
        IEnemyMovement Movement { get; }
        IHealth Health { get; }
        
        EnemyAnimations Animations { get; }
        StateMachine StateMachine { get; }
    }
}