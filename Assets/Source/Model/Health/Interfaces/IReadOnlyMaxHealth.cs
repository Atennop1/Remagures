namespace Remagures.Model.Health
{
    public interface IReadOnlyMaxHealth
    {
        int Value { get; }
        bool CanIncrease(int amount);
        bool CanDecrease(int amount);
    }
}