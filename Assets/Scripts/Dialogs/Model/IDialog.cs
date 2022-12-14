using Remagures.Dialogs.Model.Data;

namespace Remagures.Dialogs.Model
{
    public interface IDialog
    {
        string Name { get; }
        DialogLine CurrentLine { get; }
        bool CanSwitchToNextLine { get; }
    }
}