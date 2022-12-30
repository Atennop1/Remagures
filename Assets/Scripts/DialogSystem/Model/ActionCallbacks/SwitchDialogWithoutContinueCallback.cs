using Remagures.DialogSystem.View;
using Remagures.Root;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.Model.ActionCallbacks
{
    public class SwitchDialogWithoutContinueCallback : SerializedMonoBehaviour, IDialogActionCallback
    {
        [SerializeField] private string _newDialogName;
        [OdinSerialize] private DialogsListRoot _dialogsListRoot;
        
        [Space]
        [SerializeField] private DialogView _dialogView;
        [SerializeField] private Button _dialogWindowButton;

        public void Callback()
        {
            _dialogWindowButton.onClick.RemoveListener(_dialogView.NextReplica);
            _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
        }
    }
}