namespace Remagures.Model.Health
{
    public interface IMaxHealth : IReadOnlyMaxHealth
    {
        void Increase(int amount);
        void Decrease(int amount);
    }
}