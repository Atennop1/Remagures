using System;
using System.Collections.Generic;

namespace Remagures.DialogSystem.Model.Core
{
    [Serializable]
    public class DialogLine
    {
        public string Line { get; }
        public DialogSpeakerInfo SpeakerInfo { get; }
        
        public IReadOnlyList<DialogChoice> Choices { get; }
        public bool IsEnded { get; private set; }
        
        public void End()
            => IsEnded = true;
        
        public DialogLine(string line, DialogSpeakerInfo speakerInfo, IReadOnlyList<DialogChoice> choices)
        {
            Line = line ?? throw new ArgumentException("TextLine can't be null");
            Choices = choices ?? throw new ArgumentException("ChoicesList can't be null");
            SpeakerInfo = speakerInfo;
        }
    }
}