using System;
using System.Collections.Generic;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradesChain<TItem> : IUpgradesChain<TItem> where TItem: IItem
    {
        private readonly Dictionary<TItem, IUpgrade<TItem>> _itemsAndUpgrades;
        private readonly IInventory<TItem> _inventory;
        private readonly IUpgradesClient _client;

        private readonly List<TItem> _keys;

        public UpgradesChain(Dictionary<TItem, IUpgrade<TItem>> itemsAndUpgrades, IInventory<TItem> inventory, IUpgradesClient client)
        {
            _itemsAndUpgrades = itemsAndUpgrades ?? throw new ArgumentNullException(nameof(itemsAndUpgrades));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            
            _keys =  new List<TItem>(_itemsAndUpgrades.Keys);
        }

        public void Upgrade(TItem item)
        {
            var currentItem = _keys.Find(keyItem => keyItem.Equals(item));
            var upgrade = _itemsAndUpgrades[_keys.Find(keyItem => keyItem.Equals(item))];
            _client.Buy(upgrade.BuyingData);
            
            _inventory.Remove(new Cell<TItem>(currentItem));
            _inventory.Add(new Cell<TItem>(upgrade.Item));
        }

        public IUpgrade<TItem> GetNextUpgrade(TItem item)
        {
            if (!CanUpgrade(item))
                throw new InvalidOperationException("Can't advance this item");
            
            return _itemsAndUpgrades[_keys.Find(keyItem => keyItem.Equals(item))];
        }

        public bool CanUpgrade(TItem item)
        {
            var currentItem = _keys.Find(keyItem => keyItem.Equals(item));
            return currentItem != null && _client.CanBuy(_itemsAndUpgrades[currentItem].BuyingData);
        }
    }
}