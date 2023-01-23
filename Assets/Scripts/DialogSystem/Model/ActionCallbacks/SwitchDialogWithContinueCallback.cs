using Remagures.DialogSystem.View;
using Remagures.Root;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class SwitchDialogWithContinueCallback : SerializedMonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [OdinSerialize] private DialogsListRoot _dialogsListRoot;
        [SerializeField] private DialogView _dialogView;

        public void Callback()
        {
            _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
            _dialogView.Activate(_dialogsListRoot.BuiltDialogList.CurrentDialog);
        }
    }
}