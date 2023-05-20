using System;
using System.Collections.Generic;

namespace Remagures.Model.DialogSystem
{
    [Serializable]
    public sealed class DialogLine : IDialogLine
    {
        public string Text { get; }
        public IDialogSpeakerData SpeakerData { get; }
        
        public IReadOnlyList<IDialogChoice> Choices { get; }
        public bool IsEnded { get; private set; }

        public void End()
            => IsEnded = true;
        
        public DialogLine(string line, IDialogSpeakerData speakerData, IReadOnlyList<IDialogChoice> choices)
        {
            Text = line ?? throw new ArgumentException("TextLine can't be null");
            Choices = choices ?? throw new ArgumentException("ChoicesList can't be null");
            SpeakerData = speakerData;
        }
    }
}