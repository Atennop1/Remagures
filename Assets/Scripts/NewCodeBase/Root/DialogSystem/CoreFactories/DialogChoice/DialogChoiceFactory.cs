using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogChoiceFactory : SerializedMonoBehaviour, IDialogChoiceFactory
    {
        [SerializeField] private string _choiceText;
        private IDialogChoice _builtChoice;
        
        public IDialogChoice Create()
        {
            if (_builtChoice != null)
                return _builtChoice;
            
            _builtChoice = new DialogChoice(_choiceText);
            return _builtChoice;
        }
    }
}