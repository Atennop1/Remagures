using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.DialogSystem.Model.Core;
#pragma warning disable CS0660, CS0661

namespace Remagures.DialogSystem.Model
{
    [Serializable]
    public class Dialog : IDialog
    {
        public string Name { get; }
        public DialogLine CurrentLine => _lines[_currentLineIndex];
        public bool CanSwitchToNextLine => _currentLineIndex < _lines.Length - 1;
        public bool IsCurrentLineLast => _currentLineIndex == _lines.Length - 1;
        public IReadOnlyList<DialogLine> Lines => _lines.ToList();
        
        private DialogLine[] _lines { get; }
        private int _currentLineIndex;

        public Dialog(string name, DialogLine[] lines)
        {
            Name = name ?? throw new ArgumentException("Name can't be null");
            _lines = lines ?? throw new ArgumentException("Lines can't be null");
        }

        public void SwitchToNextLine()
        {
            if (!CanSwitchToNextLine)
                throw new InvalidOperationException("You already at last dialog line");
            
            _currentLineIndex++;
        }
        
        public static bool operator ==(Dialog thisDialog, Dialog anotherDialog)
        {
            if (thisDialog == null || anotherDialog == null)
                return false;

            if (thisDialog.Lines.Count != anotherDialog.Lines.Count)
                return false;
            
            for (var i = 0; i < thisDialog.Lines.Count; i++)
                if (thisDialog.Lines[i] != anotherDialog.Lines[i])
                    return false;

            return thisDialog.Name == anotherDialog.Name;
        }

        public static bool operator !=(Dialog thisDialog, Dialog anotherDialog)
            => !(thisDialog == anotherDialog);
    }
}