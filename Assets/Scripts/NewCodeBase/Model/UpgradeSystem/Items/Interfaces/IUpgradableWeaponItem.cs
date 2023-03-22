using Remagures.Model.InventorySystem;

namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradableWeaponItem : IWeaponItem
    {
        void Upgrade(UpgradeWeaponData data);
    }
}