using System;
using Remagures.DialogSystem.Model.Core;
using Remagures.DialogSystem.View;
using Remagures.Root;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks.Choice
{
    public class SwitchDialogWithContinueLineEndCallback : MonoBehaviour, ILineEndCallback
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListRoot _dialogsListRoot;
        [SerializeField] private DialogView _dialogView;

        private DialogLine _dialogLine;
        private bool _isWorked;

        private void Update()
        {
            if (!_dialogLine.IsEnded || _isWorked)
                return;

            _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
            _dialogView.Activate(_dialogsListRoot.BuiltDialogList.CurrentDialog);
            _isWorked = true;
        }

        public void Init(DialogLine line)
            => _dialogLine = line ?? throw new ArgumentNullException(nameof(line));
    }
}