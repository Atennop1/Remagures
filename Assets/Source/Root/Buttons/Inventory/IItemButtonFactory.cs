using Remagures.Model.Buttons;
using Remagures.Model.InventorySystem;

namespace Remagures.Root
{
    public interface IItemButtonFactory<TItem> where TItem: IItem
    {
        IItemButton<TItem> Create();
    }
}