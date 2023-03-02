using System;
using Remagures.Root;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class SwitchDialogWithContinueCallback : MonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListFactory dialogsListFactory;
        
        private DialogPlayer _dialogPlayer;
        private IUsableComponent _usableComponent;
        private bool _isWorked;

        private void Update()
        {
            if (!_usableComponent.IsUsed || _isWorked)
                return;

            dialogsListFactory.BuiltDialogList.SwitchToDialog(_newDialogName);
            _dialogPlayer.Play(dialogsListFactory.BuiltDialogList.CurrentDialog);
            _isWorked = true;
        }

        public void Init(DialogPlayer dialogPlayer, IUsableComponent usableComponent)
        {
            _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));
            _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
        }
    }
}