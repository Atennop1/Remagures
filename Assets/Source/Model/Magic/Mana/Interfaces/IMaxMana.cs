namespace Remagures.Model.Magic
{
    public interface IMaxMana : IReadOnlyMaxMana
    {
        void Increase(int amount);
        void Decrease(int amount);
    }
}