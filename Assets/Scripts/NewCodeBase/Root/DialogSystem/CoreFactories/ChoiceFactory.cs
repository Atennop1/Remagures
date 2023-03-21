using System.Collections.Generic;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class ChoiceFactory : SerializedMonoBehaviour
    {
        [SerializeField] private string _choiceText;
        [SerializeField] private List<IUsableComponentCallbackFactory> _callbackFactories;

        private DialogChoice _builtChoice;
        
        public DialogChoice Create()
        {
            if (_builtChoice != null)
                return _builtChoice;
            
            _builtChoice = new DialogChoice(_choiceText);

            foreach (var factory in _callbackFactories)
                factory.Create(_builtChoice);
            
            return _builtChoice;
        }
    }
}