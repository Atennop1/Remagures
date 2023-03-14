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

        private DialogsList _builtDialogList;

        public IDialogsList Create()
        {
            if (_builtDialogList != null)
                return _builtDialogList;
            
            _speakerInfoBuilders.ForEach(builder => builder.Build());
            _choiceBuilders.ForEach(builder => builder.Create());
            _dialogLineBuilders.ForEach(builder => builder.Create());
            _dialogBuilders.ForEach(builder => builder.Create());

            _builtDialogList = new DialogsList(_dialogBuilders.Select(builder => builder.Create()).ToArray(), _characterName);
            
            if (_builtDialogList.CurrentDialog == null)
                _builtDialogList.SwitchToDialog(_dialogBuilders[_idOfStartDialog].Create());

            return _builtDialogList;
        }

        private void OnDisable()
            => _builtDialogList.SaveCurrentDialog();
    }
}