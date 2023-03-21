using System;
using System.Collections.Generic;

namespace Remagures.Model.DialogSystem
{
    [Serializable]
    public class DialogLine : IDialogLine
    {
        public string Text { get; }
        public DialogSpeakerData SpeakerData { get; }
        
        public IReadOnlyList<DialogChoice> Choices { get; }
        public bool IsEnded { get; private set; }

        public void End()
            => IsEnded = true;
        
        public DialogLine(string line, DialogSpeakerData speakerData, IReadOnlyList<DialogChoice> choices)
        {
            Text = line ?? throw new ArgumentException("TextLine can't be null");
            Choices = choices ?? throw new ArgumentException("ChoicesList can't be null");
            SpeakerData = speakerData;
        }
    }
}