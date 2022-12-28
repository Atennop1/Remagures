using System;
using System.Collections.Generic;

namespace Remagures.DialogSystem.Model.Core
{
    public class DialogLine
    {
        public string Line { get; }
        public DialogSpeakerInfo SpeakerInfo { get; }
        
        public IReadOnlyList<DialogChoice> Choices { get; }

        public Action OnLineEndedAction
        {
            get => _onLineEndedAction;
            set => _onLineEndedAction = value ?? throw new ArgumentException("Action can't be null");
        }
        
        private Action _onLineEndedAction;
        
        public DialogLine(string line, DialogSpeakerInfo speakerInfo, IReadOnlyList<DialogChoice> choices)
        {
            Line = line ?? throw new ArgumentException("TextLine can't be null");
            SpeakerInfo = speakerInfo;
            
            Choices = choices ?? throw new ArgumentException("ChoicesList can't be null");
            OnLineEndedAction = () => { };
        }
    }
}