using Remagures.Root;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class DialogSwitcher : SerializedMonoBehaviour
    {
        [SerializeField] private string _newDialogName;
        [OdinSerialize] private DialogsListFactory _dialogsListFactory;

        public void Switch() 
            => _dialogsListFactory.BuiltDialogList.SwitchToDialog(_newDialogName);
    }
}