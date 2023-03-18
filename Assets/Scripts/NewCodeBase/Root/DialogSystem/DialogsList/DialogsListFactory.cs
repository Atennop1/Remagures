using System.Collections.Generic;
using System.Linq;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogsListFactory : SerializedMonoBehaviour
    {
        [SerializeField] private int _idOfStartDialog;
        [SerializeField] private string _characterName;

        [Space] [SerializeField] private List<SpeakerInfoFactory> _speakerInfoBuilders;
        [SerializeField] private List<ChoiceFactory> _choiceBuilders;

        [Space] [SerializeField] private List<DialogLineFactory> _dialogLineBuilders;
        [SerializeField] private List<DialogFactory> _dialogBuilders;

        private Dialogs _builtDialog;

        public IDialogs Create()
        {
            if (_builtDialog != null)
                return _builtDialog;
            
            _speakerInfoBuilders.ForEach(factory => factory.Build());
            _choiceBuilders.ForEach(factory => factory.Create());
            _dialogLineBuilders.ForEach(factory => factory.Create());
            _dialogBuilders.ForEach(factory => factory.Create());

            _builtDialog = new Dialogs(_dialogBuilders.Select(builder => builder.Create()).ToArray(), _characterName);
            
            if (_builtDialog.CurrentDialog == null)
                _builtDialog.SwitchCurrent(_dialogBuilders[_idOfStartDialog].Create().Name);

            return _builtDialog;
        }
    }
}