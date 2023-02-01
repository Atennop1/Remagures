﻿using System.Collections.Generic;
using System.Linq;
using Remagures.DialogSystem.Model.ActionCallbacks;
using Remagures.DialogSystem.Model.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.DialogSystem.Model.Builders
{
    public class DialogLineBuilder : SerializedMonoBehaviour
    {
        [SerializeField] private string _lineText;
        [SerializeField] private SpeakerInfoBuilder _speakerInfoBuilder;
        
        [Space]
        [SerializeField] private List<ChoiceBuilder> _choiceBuilders;
        [SerializeField] private List<IDialogActionCallback> _onLineEndedCallbacks;
        
        public DialogLine BuiltLine { get; private set; }

        public DialogLine Build()
        {
            var builtChoices = _choiceBuilders.Select(builder => builder.BuiltChoice).ToList();
            var result = new DialogLine(_lineText, _speakerInfoBuilder.BuiltInfo, builtChoices);

            foreach (var callback in _onLineEndedCallbacks)
                callback.Init(result);

            BuiltLine = result;
            return result;
        }
    }
}