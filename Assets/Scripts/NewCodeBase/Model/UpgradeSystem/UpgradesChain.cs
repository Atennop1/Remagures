using System;
using System.Collections.Generic;
using Remagures.Model.InventorySystem;
using Remagures.Model.Wallet;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradesChain<TItem> : IUpgradesChain<TItem> where TItem: IItem
    {
        private readonly List<ItemUpgradeData<TItem>> _items;
        private readonly IWallet _wallet;

        public UpgradesChain(List<ItemUpgradeData<TItem>> items)
            => _items = items ?? throw new ArgumentNullException(nameof(items));
        
        public bool CanUpgradeItem(TItem item)
        {
            var upgradeData = _items.Find(upgradeData => upgradeData.ItemWhichUpgrading.Equals(item));
            return upgradeData.ItemWhichUpgrading != null && _items.IndexOf(upgradeData) != _items.Count - 1 && _wallet.CanTake(upgradeData.Cost);
        }

        public ItemUpgradeData<TItem> GetUpgradeForItem(TItem item)
        {
            if (CanUpgradeItem(item))
                throw new ArgumentException("Item can't be upgraded");

            return _items[_items.IndexOf(_items.Find(upgradeData => upgradeData.ItemWhichUpgrading.Equals(item))) + 1];
        }
    }
}