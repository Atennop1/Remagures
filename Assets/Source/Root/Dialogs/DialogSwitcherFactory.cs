﻿using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class DialogSwitcherFactory : SerializedMonoBehaviour, IDialogSwitcherFactory
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private IDialogsListFactory _dialogsListFactory;
        private IDialogSwitcher _builtSwitcher;
        
        public IDialogSwitcher Create()
        {
            if (_builtSwitcher != null)
                return _builtSwitcher;

            _builtSwitcher = new DialogSwitcher(_dialogsListFactory.Create(), _newDialogName);
            return _builtSwitcher;
        }
    }
}