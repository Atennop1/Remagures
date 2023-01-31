using System.Collections.Generic;
using Remagures.DialogSystem.Model.Core;

namespace Remagures.DialogSystem.Model
{
    public interface IDialog
    {
        string Name { get; }
        bool CanSwitchToNextLine { get; }
        
        DialogLine CurrentLine { get; }
        IReadOnlyList<DialogLine> Lines { get; }
    }
}