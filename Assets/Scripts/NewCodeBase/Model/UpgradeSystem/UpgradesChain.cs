using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradesChain<TItem> : IUpgradesChain where TItem: IItem
    {
        private readonly List<IUpgradeLevel<TItem>> _levels;
        private readonly IInventory<TItem> _inventory;
        private readonly IUpgradesClient _client;
        
        private IUpgradeLevel<TItem> _currentLevel;

        public UpgradesChain(List<IUpgradeLevel<TItem>> levels, IInventory<TItem> inventory, IUpgradesClient client)
        {
            _levels = levels ?? throw new ArgumentNullException(nameof(levels));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _client = client ?? throw new ArgumentNullException(nameof(client));

            _currentLevel = _levels[0];
        }

        public void Advance()
        {
            if (!CanAdvance)
                throw new InvalidOperationException("Can't advance now");

            _inventory.Remove(new Cell<TItem>(_currentLevel.CurrentItem));
            _inventory.Add(new Cell<TItem>(_currentLevel.NextItem));
            
            _client.Buy(_currentLevel.BuyingData);
            _currentLevel = _levels.Find(level => level.CurrentItem.Equals(_currentLevel.NextItem));
        }

        public bool CanAdvance 
            => _levels.Any(level => level.CurrentItem.Equals(_currentLevel.NextItem));
    }
}