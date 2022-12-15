using System.Collections.Generic;
using Remagures.Dialogs.Model.ActionCallbacks;
using Remagures.Dialogs.Model.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Dialogs.Model.Builders
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
                result.OnChoiceAction += callback.Callback;

            BuiltChoice = result;
            return result;
        }
    }
}