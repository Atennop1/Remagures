using System;

namespace Remagures.DialogSystem.Model.Core
{
    [Serializable]
    public class DialogChoice
    {
        public string ChoiceText { get; }
        public bool IsUsed { get; private set; }

        public void Use()
            => IsUsed = true;

        public DialogChoice(string choiceText)
            => ChoiceText = choiceText ?? throw new ArgumentException("ChoiceText can't be null");
    }
}