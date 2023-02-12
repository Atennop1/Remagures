namespace Remagures.Model.InventorySystem
{
    public interface IInventoryItemSelector<T> where T: IItem
    {
        ICell<T> SelectedCell { get; }
        void Select(T item);
        void UnSelect();
    }
}