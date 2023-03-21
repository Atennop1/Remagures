using System.Collections.Generic;

namespace Remagures.Model.DialogSystem
{
    public interface IDialog
    {
        string Name { get; }
        bool CanSwitchToNextLine { get; }
        
        IDialogLine CurrentLine { get; }
        IReadOnlyList<IDialogLine> Lines { get; }
    }
}