using Remagures.Model.DialogSystem;
using Remagures.Model.DialogSystem.Decorators;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogChoiceWithAdditionalBehaviourFactory : SerializedMonoBehaviour, IDialogChoiceFactory
    {
        [SerializeField] private IDialogChoiceFactory _dialogChoiceFactory;
        [SerializeField] private IAdditionalBehaviourFactory _additionalBehaviourFactory;
        private IDialogChoice _builtChoice;
        
        public IDialogChoice Create()
        {
            if (_builtChoice != null)
                return _builtChoice;

            _builtChoice = new DialogChoiceWithAdditionalBehaviour(_dialogChoiceFactory.Create(), _additionalBehaviourFactory.Create());
            return _builtChoice;
        }
    }
}