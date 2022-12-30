using System;
using Remagures.DialogSystem.Model.Core;

namespace Remagures.DialogSystem.Model
{
    [Serializable]
    public class Dialog : IDialog
    {
        public string Name { get; }
        public DialogLine CurrentLine => _lines[_currentLineIndex];
        public bool CanSwitchToNextLine => _currentLineIndex < _lines.Length - 1;
        public bool IsCurrentLineLast => _currentLineIndex == _lines.Length - 1;
        
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
    }
}