using System.Collections.Generic;
using System.Linq;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class DialogLineFactory : SerializedMonoBehaviour, IDialogLineFactory
    {
        [SerializeField] private string _lineText;
        [SerializeField] private IDialogSpeakerData _speakerInfo;
        [SerializeField] private List<IDialogChoiceFactory> _choiceFactories;

        private IDialogLine _builtLine;

        public IDialogLine Create()
        {
            if (_builtLine != null)
                return _builtLine;
            
            var builtChoices = _choiceFactories.Select(builder => builder.Create()).ToList();
            _builtLine = new DialogLine(_lineText, _speakerInfo, builtChoices);
            return _builtLine;
        }
    }
}