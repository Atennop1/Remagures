using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class ChoiceBuilder : SerializedMonoBehaviour
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