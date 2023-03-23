using Remagures.Model.Wallet;
using Remagures.Tools;

namespace Remagures.Model.UpgradeSystem
{
    public readonly struct UpgradeLevelBuyingData
    {
        public readonly Currency Currency;
        public readonly int Cost;

        public UpgradeLevelBuyingData(Currency currency, int cost)
        {
            Currency = currency;
            Cost = cost.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}