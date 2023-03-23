using Remagures.Model.Wallet;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradeBuyingData
    {
        Currency Currency { get; }
        int Cost { get; }
    }
}