namespace Remagures.Model.InventorySystem
{
    public interface IInventoryItemSelector<T> where T: IItem
    {
        IReadOnlyCell<T> SelectedCell { get; }
        bool HasSelected { get; }
        
        void Select(T item);
        void UnSelect();
    }
}