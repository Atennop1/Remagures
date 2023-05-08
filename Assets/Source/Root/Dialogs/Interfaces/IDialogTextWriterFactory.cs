using Remagures.Model.DialogSystem;

namespace Remagures.Root.Dialogs
{
    public interface IDialogTextWriterFactory
    {
        IDialogTextWriter Create();
    }
}