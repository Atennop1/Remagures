using Remagures.Model.InventorySystem;

namespace Remagures.Model.UI
{
    public interface IItemButton<T> : IButton where T: IItem
    {
        void SetItem(T item);
    }
}