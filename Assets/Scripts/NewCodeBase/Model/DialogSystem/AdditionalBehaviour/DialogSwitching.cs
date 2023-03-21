using System;

namespace Remagures.Model.DialogSystem
{
    public sealed class DialogSwitching : IAdditionalBehaviour
    {
        private readonly IDialogs _dialogs;
        private readonly string _newDialogName;

        public DialogSwitching(IDialogs dialogs, string newDialogName)
        {
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
            _newDialogName = newDialogName ?? throw new ArgumentNullException(nameof(newDialogName));
        }

        public void Use()
            => _dialogs.SwitchCurrent(_newDialogName);
    }
}