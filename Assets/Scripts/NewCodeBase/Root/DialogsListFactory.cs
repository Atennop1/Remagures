using System.Collections.Generic;
using System.Linq;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class DialogsListFactory : SerializedMonoBehaviour
    {
        [SerializeField] private int _idOfStartDialog;
        [SerializeField] private string _characterName;

        [Space] [SerializeField] private List<SpeakerInfoBuilder> _speakerInfoBuilders;
        [SerializeField] private List<ChoiceBuilder> _choiceBuilders;

        [Space] [SerializeField] private List<DialogLineBuilder> _dialogLineBuilders;
        [SerializeField] private List<DialogBuilder> _dialogBuilders;

        public DialogsList BuiltDialogList { get; private set; }

        public IDialogsList Create()
        {
            if (BuiltDialogList != null)
                return BuiltDialogList;
            
            _speakerInfoBuilders.ForEach(builder => builder.Build());
            _choiceBuilders.ForEach(builder => builder.Build());
            _dialogLineBuilders.ForEach(builder => builder.Build());
            _dialogBuilders.ForEach(builder => builder.Build());

            BuiltDialogList = new DialogsList(_dialogBuilders.Select(builder => builder.Build()).ToArray(), _characterName);
            
            if (BuiltDialogList.CurrentDialog == null)
                BuiltDialogList.SwitchToDialog(_dialogBuilders[_idOfStartDialog].Build());

            return BuiltDialogList;
        }

        private void OnDisable()
            => BuiltDialogList.SaveCurrentDialog();
    }
}