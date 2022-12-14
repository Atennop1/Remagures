using System;

namespace Remagures.Dialogs.Model.Data
{
    public class DialogChoice
    {
        public string ChoiceText { get; }
        public Action OnChoiceEvent { get; }
        
        public DialogChoice(string choiceText)
        {
            ChoiceText = choiceText ?? throw new ArgumentException("ChoiceText can't be null");
            OnChoiceEvent = () => { };
        }
    }
}