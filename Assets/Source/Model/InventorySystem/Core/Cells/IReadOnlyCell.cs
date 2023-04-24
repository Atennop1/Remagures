namespace Remagures.Model.InventorySystem
{
    public interface IReadOnlyCell<T> where T: IItem
    {
        T Item { get; }
        int ItemsCount { get; }
        bool CanAddItem(T item);
    }
}