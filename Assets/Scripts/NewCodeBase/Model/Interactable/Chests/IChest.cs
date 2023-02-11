using Remagures.Model.InventorySystem;
using Remagures.SO;

namespace Remagures.Model.Interactable
{
    public interface IChest : IInteractable
    {
        bool IsOpened { get; }
        Item Item { get; }
    }
}