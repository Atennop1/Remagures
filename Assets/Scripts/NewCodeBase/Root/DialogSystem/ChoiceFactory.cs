using System.Collections.Generic;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class ChoiceFactory : SerializedMonoBehaviour
    {
        [SerializeField] private string _choiceText;
        [SerializeField] private List<IDialogActionCallback> _callbacks;

        private DialogChoice _builtChoice;
        
        public DialogChoice Build()
        {
            if (_builtChoice != null)
                return _builtChoice;
            
            var result = new DialogChoice(_choiceText);

            foreach (var callback in _callbacks)
                callback.Init(result);

            _builtChoice = result;
            return _builtChoice;
        }
    }
}