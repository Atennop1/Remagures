using System.Collections.Generic;
using System.Linq;
using Remagures.DialogSystem.Model;
using Remagures.DialogSystem.Model.Builders;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class DialogsListRoot : CompositeRoot
    {
        [SerializeField] private int _idOfStartDialog;
        [SerializeField] private string _characterName;
        
        [Space]
        [SerializeField] private List<SpeakerInfoBuilder> _speakerInfoBuilders;
        [SerializeField] private List<ChoiceBuilder> _choiceBuilders;
        
        [Space]
        [SerializeField] private List<DialogLineBuilder> _dialogLineBuilders;
        [SerializeField] private List<DialogBuilder> _dialogBuilders;

        public DialogsList BuiltDialogList { get; private set; }

        public override void Compose()
        {
            _speakerInfoBuilders.ForEach(builder => builder.Build());
            _choiceBuilders.ForEach(builder => builder.Build());
            _dialogLineBuilders.ForEach(builder => builder.Build());
            _dialogBuilders.ForEach(builder => builder.Build());

            BuiltDialogList = new DialogsList(_dialogBuilders.Select(builder => builder.BuiltDialog).ToArray(), _characterName);
            if (BuiltDialogList.CurrentDialog == null)
                BuiltDialogList.SwitchToDialog(_dialogBuilders[_idOfStartDialog].BuiltDialog);
        }

        //private void OnDisable() 
        //    => BuiltDialogList.SaveCurrentDialog();
    }
}