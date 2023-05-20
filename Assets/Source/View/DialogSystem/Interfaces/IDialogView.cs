using Remagures.Model.DialogSystem;

namespace Remagures.View.DialogSystem
{
    public interface IDialogView
    {
        void DisplayStartOfDialog();
        void DisplayEndOfDialog();
        void DisplayLineInfo(IDialogLine line);
        void DisplayChoices(IDialogLine line);
    }
}