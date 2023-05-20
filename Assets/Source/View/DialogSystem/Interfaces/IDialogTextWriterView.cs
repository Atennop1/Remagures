namespace Remagures.View.DialogSystem
{
    public interface IDialogTextWriterView
    {
        void DisplayText(string text);
        void DisplayStartOfTyping();
        void DisplayEndOfTyping();
    }
}