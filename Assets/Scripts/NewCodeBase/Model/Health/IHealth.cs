namespace Remagures.Model.Health
{
    public interface IHealth
    {
        int MaxValue { get; }
        int CurrentValue { get; }
        
        bool IsDead { get; }
        bool CanTakeDamage { get; }
        
        void TakeDamage(int amount);
        void Heal(int amount);
    }
}