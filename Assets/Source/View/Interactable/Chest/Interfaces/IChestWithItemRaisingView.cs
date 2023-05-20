using Remagures.Model.InventorySystem;

namespace Remagures.View.Interactable
{
    public interface IChestWithItemRaisingView
    {
        void EndDisplaying();
        void Display(IItem item);
    }
}