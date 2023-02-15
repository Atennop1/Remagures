namespace Remagures.Model.Magic
{
    public interface IManaUpgrade
    {
        int MaxPossibleValue { get; }
        void IncreaseMaxMana(int value);
    }
}