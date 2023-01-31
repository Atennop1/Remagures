using System;
using Remagures.DialogSystem.Model.Core;
using Remagures.Root;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks.Choice
{
    public class SwitchDialogWithoutContinueLineEndCallback : MonoBehaviour, ILineEndCallback
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListRoot _dialogsListRoot;

        private DialogLine _dialogLine;
        private bool _isWorked;

        private void Update()
        {
            if (!_dialogLine.IsEnded || _isWorked)
                return;

            _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
            _isWorked = true;
        }

        public void Init(DialogLine line)
            => _dialogLine = line ?? throw new ArgumentNullException(nameof(line));
    }
}