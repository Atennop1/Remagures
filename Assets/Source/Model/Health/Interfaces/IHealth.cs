namespace Remagures.Model.Health
{
    public interface IHealth
    {
        IReadOnlyMaxHealth Max { get; }
        int CurrentValue { get; }
        
        bool IsDead { get; }
        bool CanTakeDamage { get; }
        
        void TakeDamage(int amount);
        void Heal(int amount);
    }
}