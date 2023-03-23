namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradesClient
    {
        bool CanBuy(UpgradeLevelBuyingData buyingData);
        void Buy(UpgradeLevelBuyingData buyingData);
    }
}