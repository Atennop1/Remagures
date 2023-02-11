namespace Remagures.Model.InventorySystem
{
    public interface IUniqueItem : IBaseItemComponent
    {
        public UniqueType UniqueType { get; }
    }
}
