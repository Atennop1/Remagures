namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradesClient
    {
        bool CanBuy(IUpgradeBuyingData buyingData);
        void Buy(IUpgradeBuyingData buyingData);
    }
}