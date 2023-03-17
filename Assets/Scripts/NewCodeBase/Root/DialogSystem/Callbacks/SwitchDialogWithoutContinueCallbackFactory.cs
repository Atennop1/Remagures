using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class SwitchDialogWithoutContinueCallbackFactory : SerializedMonoBehaviour, IUsableComponentCallbackFactory
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListFactory _dialogsListFactory;
        private SwitchDialogWithoutContinueCallback _builtCallback;
        
        public void Create(IUsableComponent component)
        {
            if (_builtCallback != null)
                return;
            
            _builtCallback = new SwitchDialogWithoutContinueCallback(component, _dialogsListFactory.Create(), _newDialogName);
        }
    }
}