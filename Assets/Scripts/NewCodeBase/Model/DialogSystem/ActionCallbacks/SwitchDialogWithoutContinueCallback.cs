using System;
using Remagures.Root;

namespace Remagures.Model.DialogSystem
{
    public sealed class SwitchDialogWithoutContinueCallback : IUpdatable
    {
        private readonly IUsableComponent _usableComponent;
        private readonly IDialogs _dialogs;
        private readonly string _newDialogName;

        private bool _hasWorked;

        public SwitchDialogWithoutContinueCallback(IUsableComponent usableComponent, IDialogs dialogs, string newDialogName)
        {
            _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
            _newDialogName = newDialogName ?? throw new ArgumentNullException(nameof(newDialogName));
        }

        public void Update()
        {
            if (!_usableComponent.IsUsed || _hasWorked)
                return;

            _dialogs.SwitchToDialog(_newDialogName);
            _hasWorked = true;
        }
    }
}