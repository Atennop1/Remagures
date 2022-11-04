namespace Remagures.AI.Enemies.Abstraction
{
    public interface IEnemyWithTarget : IEnemy
    {
        EnemyTargetData TargetData { get; }
    }
}