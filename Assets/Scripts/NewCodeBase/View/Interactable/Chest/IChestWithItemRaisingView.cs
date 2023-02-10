using Remagures.SO;

namespace Remagures.View.Interactable
{
    public interface IChestWithItemRaisingView
    {
        void EndDisplaying();
        void Display(BaseInventoryItem item);
    }
}