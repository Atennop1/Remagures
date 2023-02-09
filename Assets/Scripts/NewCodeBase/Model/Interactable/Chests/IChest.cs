using Remagures.SO;

namespace Remagures.Model.Interactable
{
    public interface IChest : IInteractable
    {
        bool IsOpened { get; }
        BaseInventoryItem Item { get; }
    }
}