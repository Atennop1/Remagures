using Remagures.Model.DialogSystem;
using Remagures.Model.DialogSystem.Decorators;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogLineWithAdditionalBehaviourFactory : SerializedMonoBehaviour, IDialogLineFactory
    {
        [SerializeField] private IDialogLineFactory _dialogLineFactory;
        [SerializeField] private IAdditionalBehaviourFactory _additionalBehaviourFactory;
        private IDialogLine _builtLine;
        
        public IDialogLine Create()
        {
            if (_builtLine != null)
                return _builtLine;

            _builtLine = new DialogLineWithAdditionalBehaviour(_dialogLineFactory.Create(), _additionalBehaviourFactory.Create());
            return _builtLine;
        }
    }
}