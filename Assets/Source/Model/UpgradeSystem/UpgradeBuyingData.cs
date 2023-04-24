using Remagures.Model.Wallet;
using Remagures.Tools;

namespace Remagures.Model.UpgradeSystem
{
    public readonly struct UpgradeBuyingData : IUpgradeBuyingData
    {
        public Currency Currency { get; }
        public int Cost { get; }

        public UpgradeBuyingData(Currency currency, int cost)
        {
            Currency = currency;
            Cost = cost.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}