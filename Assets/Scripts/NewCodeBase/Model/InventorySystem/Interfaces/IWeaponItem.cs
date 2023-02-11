namespace Remagures.Model.InventorySystem
{
    public interface IWeaponItem : IUsableItem
    {
        int Damage { get; }
    }
}
