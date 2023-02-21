namespace Remagures.Model.AI.Enemies
{
    public interface IAttackingEnemyAnimations : IEnemyAnimations
    {
        void SetIsAttacking(bool isActive);
    }
}