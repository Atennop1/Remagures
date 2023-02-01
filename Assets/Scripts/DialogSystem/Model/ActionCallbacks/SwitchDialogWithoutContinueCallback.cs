using System;
using Remagures.DialogSystem.Model.Core;
using Remagures.Root;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class SwitchDialogWithoutContinueCallback : MonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListRoot _dialogsListRoot;

        private IUsableComponent _usableComponent;
        private bool _isWorked;

        private void Update()
        {
            if (!_usableComponent.IsUsed || _isWorked)
                return;

            _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
            _isWorked = true;
        }

        public void Init(IUsableComponent usableComponent)
            => _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
    }
}