using Remagures.Model.DialogSystem;
using Remagures.Root.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class DialogSwitcherFactory : SerializedMonoBehaviour
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListFactory _dialogsListFactory;
        private DialogSwitcher _builtSwitcher;
        
        public DialogSwitcher Create()
        {
            if (_builtSwitcher != null)
                return _builtSwitcher;

            _builtSwitcher = new DialogSwitcher(_dialogsListFactory.Create(), _newDialogName);
            return _builtSwitcher;
        }
    }
}