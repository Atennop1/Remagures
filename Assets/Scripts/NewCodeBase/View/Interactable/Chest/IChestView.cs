namespace Remagures.View.Interactable
{
    public interface IChestView
    {
        void DisplayClosed();
        void DisplayOpened();
        void DisplayItemName(string name);
    }
}