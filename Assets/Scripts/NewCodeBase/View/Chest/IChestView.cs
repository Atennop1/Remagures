namespace Remagures.View.Chest
{
    public interface IChestView
    {
        void DisplayClosed();
        void DisplayOpened();
        void DisplayItemName(string name);
    }
}