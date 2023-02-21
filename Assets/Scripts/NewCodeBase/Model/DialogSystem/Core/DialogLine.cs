using System;
using System.Collections.Generic;

namespace Remagures.Model.DialogSystem
{
    [Serializable]
    public class DialogLine : IUsableComponent
    {
        public string Text { get; }
        public DialogSpeakerData SpeakerData { get; }
        
        public IReadOnlyList<DialogChoice> Choices { get; }
        public bool IsUsed { get; private set; }
        
        public void End()
            => IsUsed = true;
        
        public DialogLine(string line, DialogSpeakerData speakerData, IReadOnlyList<DialogChoice> choices)
        {
            Text = line ?? throw new ArgumentException("TextLine can't be null");
            Choices = choices ?? throw new ArgumentException("ChoicesList can't be null");
            SpeakerData = speakerData;
        }
    }
}