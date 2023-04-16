using System;

namespace Remagures.Model.DialogSystem
{
    public sealed class DialogSwitcher : IDialogSwitcher
    {
        private readonly string _newDialogName;
        private readonly IDialogs _dialogs;

        public DialogSwitcher(IDialogs dialogs, string newDialogName)
        {
            _newDialogName = newDialogName ?? throw new ArgumentNullException(nameof(newDialogName));
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
        }

        public void Switch() 
            => _dialogs.SwitchCurrent(_newDialogName);
    }
}