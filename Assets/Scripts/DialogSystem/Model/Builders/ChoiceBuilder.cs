using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.DialogSystem
{
    public class ChoiceBuilder : SerializedMonoBehaviour
    {
        [SerializeField] private string _choiceText;
        [SerializeField] private List<IDialogActionCallback> _callbacks;

        public DialogChoice BuiltChoice { get; private set; }
        
        public DialogChoice Build()
        {
            var result = new DialogChoice(_choiceText);

            foreach (var callback in _callbacks)
                callback.Init(result);

            BuiltChoice = result;
            return result;
        }
    }
}