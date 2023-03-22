using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradableArmorItem : IArmorItem
    {
        void Upgrade(UpgradeArmorItemData data);
    }
}