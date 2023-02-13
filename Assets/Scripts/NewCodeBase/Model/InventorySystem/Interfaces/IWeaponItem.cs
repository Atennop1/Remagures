namespace Remagures.Model.InventorySystem
{
    public interface IWeaponItem : IUsableItem, IDisplayableItem
    {
        int Damage { get; }
    }
}
