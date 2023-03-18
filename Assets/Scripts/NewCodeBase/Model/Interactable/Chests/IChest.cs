using Remagures.Model.InventorySystem;

namespace Remagures.Model.Interactable
{
    public interface IChest : IInteractable
    {
        bool IsOpened { get; }
        IItem Item { get; }
    }
}