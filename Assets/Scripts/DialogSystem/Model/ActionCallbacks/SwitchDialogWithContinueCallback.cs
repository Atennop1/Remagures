using System;
using Remagures.DialogSystem.Model.Core;
using Remagures.DialogSystem.View;
using Remagures.Root;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class SwitchDialogWithContinueCallback : MonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListRoot _dialogsListRoot;
        [SerializeField] private DialogView _dialogView;

        private IUsableComponent _usableComponent;
        private bool _isWorked;

        private void Update()
        {
            if (!_usableComponent.IsUsed || _isWorked)
                return;

            _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
            _dialogView.Activate(_dialogsListRoot.BuiltDialogList.CurrentDialog);
            _isWorked = true;
        }

        public void Init(IUsableComponent usableComponent)
            => _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
    }
}