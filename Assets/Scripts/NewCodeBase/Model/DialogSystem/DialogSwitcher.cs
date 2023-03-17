using System;

namespace Remagures.Model.DialogSystem
{
    public sealed class DialogSwitcher
    {
        private readonly string _newDialogName;
        private readonly IDialogsList _dialogsList;

        public DialogSwitcher(IDialogsList dialogsList, string newDialogName)
        {
            _newDialogName = newDialogName ?? throw new ArgumentNullException(nameof(newDialogName));
            _dialogsList = dialogsList ?? throw new ArgumentNullException(nameof(dialogsList));
        }

        public void Switch() 
            => _dialogsList.SwitchToDialog(_newDialogName);
    }
}