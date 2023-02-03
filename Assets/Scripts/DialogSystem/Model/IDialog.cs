using System.Collections.Generic;

namespace Remagures.DialogSystem
{
    public interface IDialog
    {
        string Name { get; }
        bool CanSwitchToNextLine { get; }
        
        DialogLine CurrentLine { get; }
        IReadOnlyList<DialogLine> Lines { get; }
    }
}