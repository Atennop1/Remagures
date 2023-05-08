using Remagures.Model.DialogSystem;

namespace Remagures.Root.Dialogs
{
    public interface IDialogFactory
    {
        IDialog Create();
    }
}