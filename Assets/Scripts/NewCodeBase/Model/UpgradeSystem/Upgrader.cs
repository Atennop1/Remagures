using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Model.Wallet;

namespace Remagures.Model.UpgradeSystem
{
    public class Upgrader : IUpgrader
    {
        private readonly IUpgradesChain _chain;
        private readonly IInventory<IItem> _inventory;
        private readonly IWallet _wallet;

        public void Upgrade(IItem item)
        {
            if (!CanUpgradeItem(item))
                throw new ArgumentException("Can't upgrade this item");

            var upgradeData = _chain.GetUpgradedItemData(item);
            _wallet.Take(upgradeData.Cost);
            
            _inventory.Remove(new Cell<IItem>(item));
            _inventory.Add(new Cell<IItem>(upgradeData.Item));
        }

        public bool CanUpgradeItem(IItem item)
        {
            if (!_chain.CanUpgradeItem(item))
                return false; 
            
            var upgradeData = _chain.GetUpgradedItemData(item);
            return _wallet.CanTake(upgradeData.Cost) && _inventory.Cells.Any(cell => cell.Item.Equals(item));
        }

        public UpgradeItemData GetUpgradedItemData(IItem item)
        {
            if (!CanUpgradeItem(item))
                throw new ArgumentException("Can't upgrade this item");

            return _chain.GetUpgradedItemData(item);
        }
    }
}