using System;

namespace Remagures.Dialogs.Model.Core
{
    public class DialogChoice
    {
        public string ChoiceText { get; }

        public Action OnChoiceAction
        {
            get => _onChoiceAction;
            set => _onChoiceAction = value ?? throw new ArgumentException("Action can't be null");
        }
        
        private Action _onChoiceAction;
        
        public DialogChoice(string choiceText)
        {
            ChoiceText = choiceText ?? throw new ArgumentException("ChoiceText can't be null");
            OnChoiceAction = () => { };
        }
    }
}