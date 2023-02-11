using Remagures.Model.InventorySystem;
using Remagures.SO;

namespace Remagures.View.Interactable
{
    public interface IChestWithItemRaisingView
    {
        void EndDisplaying();
        void Display(Item item);
    }
}