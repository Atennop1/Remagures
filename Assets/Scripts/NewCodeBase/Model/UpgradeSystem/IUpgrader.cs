using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgrader
    {
        void Upgrade(IItem item);
        bool CanUpgradeItem(IItem item);
        UpgradeItemData GetUpgradedItemData(IItem item);
    }
}