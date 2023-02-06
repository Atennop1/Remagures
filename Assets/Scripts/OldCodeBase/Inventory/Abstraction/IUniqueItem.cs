namespace Remagures.Inventory
{
    public interface IUniqueItem : IBaseItemComponent
    {
        public UniqueType UniqueType { get; }
    }
}
