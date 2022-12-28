using Remagures.DialogSystem.Model.Core;

namespace Remagures.DialogSystem.Model
{
    public interface IDialog
    {
        string Name { get; }
        DialogLine CurrentLine { get; }
        bool CanSwitchToNextLine { get; }
    }
}