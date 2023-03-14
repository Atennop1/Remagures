using System;
using Remagures.Root;

namespace Remagures.Model.DialogSystem
{
    public sealed class SwitchDialogWithoutContinueCallback : IUpdatable
    {
        private readonly IUsableComponent _usableComponent;
        private readonly IDialogsList _dialogsList;
        private readonly string _newDialogName;

        private bool _hasWorked;

        public SwitchDialogWithoutContinueCallback(IUsableComponent usableComponent, IDialogsList dialogsList, string newDialogName)
        {
            _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
            _dialogsList = dialogsList ?? throw new ArgumentNullException(nameof(dialogsList));
            _newDialogName = newDialogName ?? throw new ArgumentNullException(nameof(newDialogName));
        }

        public void Update()
        {
            if (!_usableComponent.IsUsed || _hasWorked)
                return;

            _dialogsList.SwitchToDialog(_newDialogName);
            _hasWorked = true;
        }
    }
}