using System.Collections.Generic;
using Remagures.DialogSystem.Model.ActionCallbacks;
using Remagures.DialogSystem.Model.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.DialogSystem.Model.Builders
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