using System.Collections.Generic;
using System.Linq;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogLineFactory : SerializedMonoBehaviour
    {
        [SerializeField] private string _lineText;
        [SerializeField] private SpeakerInfoFactory speakerInfoFactory;
        
        [Space]
        [SerializeField] private List<ChoiceFactory> _choiceBuilders;
        [SerializeField] private List<IDialogActionCallback> _onLineEndedCallbacks;
        
        public DialogLine BuiltLine { get; private set; }

        public DialogLine Build()
        {
            var builtChoices = _choiceBuilders.Select(builder => builder.Build()).ToList();
            var result = new DialogLine(_lineText, speakerInfoFactory._builtData, builtChoices);

            foreach (var callback in _onLineEndedCallbacks)
                callback.Init(result);

            BuiltLine = result;
            return result;
        }
    }
}