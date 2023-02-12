using Remagures.Model.InventorySystem;

namespace Remagures.View.Inventory
{
    public interface IInventoryOfSelectablesView
    {
        void DisplaySelected(ICell<IItem> cell);
    }
}