using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS0660, CS0661

namespace Remagures.Model.DialogSystem
{
    [Serializable]
    public sealed class Dialog : IDialog
    {
        public string Name { get; }
        public IDialogLine CurrentLine => _lines[_currentLineIndex];
        
        public bool CanSwitchToNextLine => _currentLineIndex < _lines.Count - 1;
        public bool IsCurrentLineLast => _currentLineIndex == _lines.Count - 1;
        public IReadOnlyList<IDialogLine> Lines => _lines.ToList();

        private List<IDialogLine> _lines;
        private int _currentLineIndex;

        public Dialog(string name, IEnumerable<IDialogLine> lines)
        {
            Name = name ?? throw new ArgumentException("Name can't be null");
            _lines = lines.ToList();
        }

        public void SwitchToNextLine()
        {
            if (!CanSwitchToNextLine)
                throw new InvalidOperationException("You already at last dialog line");

            _currentLineIndex++;
        }
        
        public static bool operator ==(Dialog thisDialog, Dialog anotherDialog)
        {
            if (thisDialog is null && anotherDialog is null)
                return true;

            if (thisDialog is null || anotherDialog is null)
                return false;
            
            if (thisDialog.Lines.Count != anotherDialog.Lines.Count)
                return false;

            for (var i = 0; i < thisDialog.Lines.Count; i++)
            {
                var firstLine = thisDialog.Lines[i];
                var secondLine = anotherDialog.Lines[i];
                
                if (firstLine.Text != secondLine.Text || firstLine.SpeakerData.SpeakerName != secondLine.SpeakerData.SpeakerName)
                    return false;
            }

            return thisDialog.Name == anotherDialog.Name;
        }

        public static bool operator !=(Dialog thisDialog, Dialog anotherDialog)
            => !(thisDialog == anotherDialog);
    }
}