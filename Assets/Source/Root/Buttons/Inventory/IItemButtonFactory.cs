using Remagures.Model.InventorySystem;
using Remagures.Model.UI;

namespace Remagures.Root
{
    public interface IItemButtonFactory<TItem> where TItem: IItem
    {
        IItemButton<TItem> Create();
    }
}