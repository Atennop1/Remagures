namespace Remagures.Model.AI.Enemies
{
    public interface IEnemyWithTarget : IEnemy
    {
        EnemyTargetData TargetData { get; }
    }
}