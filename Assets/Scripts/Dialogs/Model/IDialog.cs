using Remagures.Dialogs.Model.Core;

namespace Remagures.Dialogs.Model
{
    public interface IDialog
    {
        string Name { get; }
        DialogLine CurrentLine { get; }
        bool CanSwitchToNextLine { get; }
    }
}