using System;

namespace Remagures.Model.DialogSystem
{
    public sealed class DialogChoiceWithAdditionalBehaviour : IDialogChoice
    {
        public string Text => _dialogChoice.Text;
        public bool IsUsed => _dialogChoice.IsUsed;

        private readonly IDialogChoice _dialogChoice;
        private readonly IAdditionalBehaviour _additionalBehaviour;

        public DialogChoiceWithAdditionalBehaviour(IDialogChoice dialogChoice, IAdditionalBehaviour additionalBehaviour)
        {
            _dialogChoice = dialogChoice ?? throw new ArgumentNullException(nameof(dialogChoice));
            _additionalBehaviour = additionalBehaviour ?? throw new ArgumentNullException(nameof(additionalBehaviour));
        }

        public void Use()
        {
            _additionalBehaviour.Use();
            _dialogChoice.Use();
        }
    }
}