﻿using System.Collections.Generic;

namespace Remagures.Model.DialogSystem
{
    public interface IDialogLine
    {
        string Text { get; }
        IDialogSpeakerData SpeakerData { get; }
        IReadOnlyList<IDialogChoice> Choices { get; }
        
        bool IsEnded { get; }
        void End();
    }
}