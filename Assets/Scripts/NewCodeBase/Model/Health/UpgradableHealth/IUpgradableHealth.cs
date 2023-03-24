namespace Remagures.Model.Health
{
    public interface IUpgradableHealth : IHealth
    {
        bool CanUpgrade(int value);
        void UpgradeMaxHealth(int value);
    }
}