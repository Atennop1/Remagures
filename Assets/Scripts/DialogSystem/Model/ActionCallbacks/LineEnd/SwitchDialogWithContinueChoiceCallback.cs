using System;
using Remagures.DialogSystem.Model.Core;
using Remagures.DialogSystem.View;
using Remagures.Root;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class SwitchDialogWithContinueChoiceCallback : MonoBehaviour, IChoiceCallback
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListRoot _dialogsListRoot;
        [SerializeField] private DialogView _dialogView;

        private DialogChoice _dialogChoice;
        private bool _isWorked;

        private void Update()
        {
            if (!_dialogChoice.IsUsed || _isWorked)
                return;

            _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
            _dialogView.Activate(_dialogsListRoot.BuiltDialogList.CurrentDialog);
            _isWorked = true;
        }

        public void Init(DialogChoice choice)
            => _dialogChoice = choice ?? throw new ArgumentNullException(nameof(choice));
    }
}