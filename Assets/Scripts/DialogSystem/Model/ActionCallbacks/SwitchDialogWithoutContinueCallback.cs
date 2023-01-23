using Remagures.Root;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class SwitchDialogWithoutContinueCallback : SerializedMonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [OdinSerialize] private DialogsListRoot _dialogsListRoot;

        public void Callback()
            => _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
    }
}