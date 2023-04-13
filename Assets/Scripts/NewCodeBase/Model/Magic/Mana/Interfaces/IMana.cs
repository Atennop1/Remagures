namespace Remagures.Model.Magic
{
    public interface IMana
    {
        int CurrentValue { get; }
        IReadOnlyMaxMana Max { get; }

        void Increase(int value);
        void Decrease(int value);
    }
}