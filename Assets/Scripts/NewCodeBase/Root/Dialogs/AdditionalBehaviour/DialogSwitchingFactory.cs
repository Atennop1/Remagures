using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class DialogSwitchingFactory : SerializedMonoBehaviour, IAdditionalBehaviourFactory
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListFactory _dialogsListFactory;
        private IAdditionalBehaviour _builtBehaviour;
        
        public IAdditionalBehaviour Create()
        {
            if (_builtBehaviour != null)
                return _builtBehaviour;
            
            _builtBehaviour = new DialogSwitching(_dialogsListFactory.Create(), _newDialogName);
            return _builtBehaviour;
        }
    }
}