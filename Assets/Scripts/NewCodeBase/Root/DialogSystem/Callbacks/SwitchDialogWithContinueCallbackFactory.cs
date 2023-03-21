using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class SwitchDialogWithContinueCallbackFactory : SerializedMonoBehaviour, IUsableComponentCallbackFactory
    {
        [SerializeField] private string _newDialogName;
        [SerializeField] private DialogsListFactory _dialogsListFactory;
        [SerializeField] private DialogPlayerFactory _dialogPlayerFactory;
        private UsableComponentWithDialogSwitchingAndContinue _builtCallback;
        
        public void Create(IUsableComponent component)
        {
            if (_builtCallback != null)
                    return;
            
            _builtCallback = new UsableComponentWithDialogSwitchingAndContinue(component, _dialogPlayerFactory.Create(), _dialogsListFactory.Create(), _newDialogName);
        }
    }
}