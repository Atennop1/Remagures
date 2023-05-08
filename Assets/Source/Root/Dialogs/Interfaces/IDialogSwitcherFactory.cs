using Remagures.Model.DialogSystem;

namespace Remagures.Root.Dialogs
{
    public interface IDialogSwitcherFactory
    {
        IDialogSwitcher Create();
    }
}