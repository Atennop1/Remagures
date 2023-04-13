namespace Remagures.Model.InventorySystem
{
    public interface IWeaponItem : IDisplayableItem
    {
        int Damage { get; }
        int UsingCooldownInMilliseconds { get; }
        int AttackingTimeInMilliseconds { get; }
    }
}
