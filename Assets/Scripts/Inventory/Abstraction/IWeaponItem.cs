namespace Remagures.Inventory.Abstraction
{
    public interface IWeaponItem : IBaseItemComponent
    {
        public int Damage { get; }
    }
}
