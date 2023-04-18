using Remagures.Model.Attacks;

namespace Remagures.Model.InventorySystem
{
    public interface IWeaponItem : IDisplayableItem
    {
        int Damage { get; }
        IAttack Attack { get; }
    }
}
