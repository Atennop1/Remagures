using System;
using System.Collections.Generic;

namespace Remagures.Model.DialogSystem.Decorators
{
    public sealed class DialogLineWithAdditionalBehaviour : IDialogLine
    {
        public string Text => _dialogLine.Text;
        public DialogSpeakerData SpeakerData => _dialogLine.SpeakerData;
        public IReadOnlyList<IDialogChoice> Choices => _dialogLine.Choices;

        public bool IsEnded => _dialogLine.IsEnded;
        
        private readonly IDialogLine _dialogLine;
        private readonly IAdditionalBehaviour _additionalBehaviour;

        public DialogLineWithAdditionalBehaviour(IDialogLine dialogLine, IAdditionalBehaviour additionalBehaviour)
        {
            _dialogLine = dialogLine ?? throw new ArgumentNullException(nameof(dialogLine));
            _additionalBehaviour = additionalBehaviour ?? throw new ArgumentNullException(nameof(additionalBehaviour));
        }

        public void End()
        {
            _additionalBehaviour.Use();
            _dialogLine.End();
        }
    }
}