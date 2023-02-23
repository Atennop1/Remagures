using System;
using System.Collections.Generic;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradesChain : IUpgradesChain
    {
        private readonly List<UpgradeItemData> _items;

        public UpgradesChain(List<UpgradeItemData> items)
            => _items = items ?? throw new ArgumentNullException(nameof(items));
        
        public bool CanUpgradeItem(IItem item)
        {
            var upgradeData = _items.Find(upgradeData => upgradeData.Item.Equals(item));
            return upgradeData.Item != null && _items.IndexOf(upgradeData) != _items.Count - 1;
        }

        public UpgradeItemData GetUpgradedItemData(IItem item)
        {
            if (CanUpgradeItem(item))
                throw new ArgumentException("Item can't be upgraded");

            return _items[_items.IndexOf(_items.Find(upgradeData => upgradeData.Item.Equals(item))) + 1];
        }
    }
}