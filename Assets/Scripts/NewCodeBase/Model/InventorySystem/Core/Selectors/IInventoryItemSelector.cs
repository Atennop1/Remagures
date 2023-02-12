namespace Remagures.Model.InventorySystem
{
    public interface IInventoryItemSelector<T> where T: IItem
    {
        IReadOnlyCell<T> SelectedCell { get; }
        void Select(T item);
        void UnSelect();
    }
}