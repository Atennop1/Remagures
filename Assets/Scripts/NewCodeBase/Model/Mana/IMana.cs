namespace Remagures.Model
{
    public interface IMana
    {
        int CurrentValue { get; }
        int MaxValue { get; }

        void Increase(int value);
        void Decrease(int value);
    }
}