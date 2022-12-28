using Remagures.Root;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Remagures.DialogSystem.Model
{
    public class DialogSwitcher : SerializedMonoBehaviour
    {
        [SerializeField] private string _newDialogName;
        [OdinSerialize] private DialogsListRoot _dialogsListRoot;

        public void Switch() => _dialogsListRoot.BuiltDialogList.SwitchToDialog(_newDialogName);
    }
}