using System;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradeLevel<TItem> : IUpgradeLevel<TItem> where TItem: IItem
    {
        public TItem UpgradedItem { get; }
        public UpgradeLevelBuyingData BuyingData { get; }

        public UpgradeLevel(TItem nextItem, UpgradeLevelBuyingData buyingData)
        {
            UpgradedItem = nextItem ?? throw new ArgumentNullException(nameof(nextItem));
            BuyingData = buyingData;
        }
    }
}