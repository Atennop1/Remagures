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
        [SerializeField] private List<IUsableComponentCallbackFactory> _callbacksFactories;

        private DialogLine _builtLine;

        public DialogLine Create()
        {
            var builtChoices = _choiceBuilders.Select(builder => builder.Create()).ToList();
            _builtLine = new DialogLine(_lineText, speakerInfoFactory._builtData, builtChoices);

            foreach (var factory in _callbacksFactories)
                factory.Create(_builtLine);
            
            return _builtLine;
        }
    }
}