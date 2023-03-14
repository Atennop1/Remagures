using Remagures.Root;
using Remagures.Root.DialogSystem;
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
            => _dialogsListFactory.Create().SwitchToDialog(_newDialogName);
    }
}