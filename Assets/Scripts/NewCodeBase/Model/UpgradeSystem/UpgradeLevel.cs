using System;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradeLevel<TItem> : IUpgradeLevel<TItem> where TItem: IItem
    {
        public TItem CurrentItem { get; }
        public TItem NextItem { get; }
        public UpgradeLevelBuyingData BuyingData { get; }

        public UpgradeLevel(TItem currentItem, TItem nextItem, UpgradeLevelBuyingData buyingData)
        {
            CurrentItem = currentItem ?? throw new ArgumentNullException(nameof(currentItem));
            NextItem = nextItem ?? throw new ArgumentNullException(nameof(nextItem));
            BuyingData = buyingData;
        }
    }
}