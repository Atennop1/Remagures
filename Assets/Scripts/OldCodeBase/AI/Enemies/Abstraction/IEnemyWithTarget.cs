namespace Remagures.AI.Enemies
{
    public interface IEnemyWithTarget : IEnemy
    {
        EnemyTargetData TargetData { get; }
    }
}