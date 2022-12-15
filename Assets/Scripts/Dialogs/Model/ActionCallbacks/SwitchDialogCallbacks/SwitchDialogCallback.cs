using Remagures.Root.DialogRoots;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Remagures.Dialogs.Model.ActionCallbacks.SwitchDialogCallbacks
{
    public class SwitchDialogCallback<T> : SerializedMonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [OdinSerialize] private DialogsListRoot<T> _dialogsListRoot;

        public void Callback() => _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
    }
}