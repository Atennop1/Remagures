namespace Remagures.Model.Magic
{
    public interface IReadOnlyMaxMana
    {
        int Value { get; }
        bool CanIncrease(int amount);
        bool CanDecrease(int amount);
    }
}