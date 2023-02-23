using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradesChain
    {
        bool CanUpgradeItem(IItem item);
        UpgradeItemData GetUpgradedItemData(IItem item);
    }
}