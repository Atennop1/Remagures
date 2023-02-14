namespace Remagures.Model.Health.HealthUpgrade
{
    public interface IHealthUpgrade
    {
        int MaxPossibleHealth { get; }
        void IncreaseMaxHealth(int value);
    }
}