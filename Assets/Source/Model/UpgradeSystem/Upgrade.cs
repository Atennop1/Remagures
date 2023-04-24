using System;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class Upgrade<TItem> : IUpgrade<TItem> where TItem: IItem
    {
        public TItem Item { get; }
        public IUpgradeBuyingData BuyingData { get; }

        public Upgrade(TItem nextItem, IUpgradeBuyingData buyingData)
        {
            Item = nextItem ?? throw new ArgumentNullException(nameof(nextItem));
            BuyingData = buyingData;
        }
    }
}