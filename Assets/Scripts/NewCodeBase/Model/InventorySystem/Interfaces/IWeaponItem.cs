namespace Remagures.Model.InventorySystem
{
    public interface IWeaponItem : IUsableItem, IDisplayableItem
    {
        int UsingCooldownInMilliseconds { get; }
        int Damage { get; }
    }
}
