using System;
using System.Collections.Generic;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradesChain<TItem> : IUpgradesChain<TItem> where TItem: IItem
    {
        private readonly List<IUpgradeLevel<TItem>> _levels;
        private readonly IInventory<TItem> _inventory;
        private readonly IUpgradesClient _client;

        public UpgradesChain(List<IUpgradeLevel<TItem>> levels, IInventory<TItem> inventory, IUpgradesClient client)
        {
            _levels = levels ?? throw new ArgumentNullException(nameof(levels));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public void Advance(TItem item)
        {
            var currentLevel = GetCurrentLevel(item);
            _client.Buy(currentLevel.BuyingData);
            
            _inventory.Remove(new Cell<TItem>(currentLevel.CurrentItem));
            _inventory.Add(new Cell<TItem>(currentLevel.NextItem));
        }

        public IUpgradeLevel<TItem> GetCurrentLevel(TItem item)
        {
            if (!CanAdvance(item))
                throw new InvalidOperationException("Can't advance this item");
            
            return _levels.Find(level => level.CurrentItem.Equals(item));
        }

        public bool CanAdvance(TItem item)
        {
            var currentLevel = _levels.Find(level => level.CurrentItem.Equals(item));
            return currentLevel != null && _client.CanBuy(currentLevel.BuyingData);
        }
    }
}