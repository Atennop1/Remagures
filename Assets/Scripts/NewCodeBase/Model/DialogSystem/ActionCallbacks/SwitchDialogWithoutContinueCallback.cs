using System;
using Remagures.Root;
using Remagures.Root.DialogSystem;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class SwitchDialogWithoutContinueCallback : MonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListFactory dialogsListFactory;

        private IUsableComponent _usableComponent;
        private bool _isWorked;

        private void Update()
        {
            if (!_usableComponent.IsUsed || _isWorked)
                return;

            dialogsListFactory.BuiltDialogList.SwitchToDialog(_newDialogName);
            _isWorked = true;
        }

        public void Init(IUsableComponent usableComponent)
            => _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
    }
}