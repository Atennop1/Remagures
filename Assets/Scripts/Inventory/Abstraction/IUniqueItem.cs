namespace Remagures.Inventory.Abstraction
{
    public interface IUniqueItem : IBaseItemComponent
    {
        public UniqueType UniqueType { get; }
    }
}
