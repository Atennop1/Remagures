using System.Collections.Generic;
using System.Linq;
using Remagures.Dialogs.Model;
using Remagures.Dialogs.Model.Builders;
using UnityEngine;

namespace Remagures.Root.DialogRoots
{
    public class DialogsListRoot<T> : CompositeRoot
    {
        [SerializeField] private int _idOfStartDialog;
        [SerializeField] private List<SpeakerInfoBuilder> _speakerInfoBuilders;
        [SerializeField] private List<ChoiceBuilder> _choiceBuilders;
        
        [Space]
        [SerializeField] private List<DialogLineBuilder> _dialogLineBuilders;
        [SerializeField] private List<DialogBuilder> _dialogBuilders;

        public DialogsList<T> BuiltDialogList { get; private set; }

        public override void Compose()
        {
            _speakerInfoBuilders.ForEach(builder => builder.Build());
            _choiceBuilders.ForEach(builder => builder.Build());
            _dialogLineBuilders.ForEach(builder => builder.Build());
            _dialogBuilders.ForEach(builder => builder.Build());

            BuiltDialogList = new DialogsList<T>(_dialogBuilders.Select(builder => builder.BuiltDialog).ToArray());
            if (BuiltDialogList.CurrentDialog == null)
                BuiltDialogList.SwitchToDialog(_dialogBuilders[_idOfStartDialog].BuiltDialog);
        }

        private void OnDisable() => BuiltDialogList.SaveCurrentDialog();
    }
    
    public interface IDialogCharacter { }
}