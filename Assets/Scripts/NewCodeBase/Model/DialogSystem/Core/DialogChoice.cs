using System;

namespace Remagures.Model.DialogSystem
{
    [Serializable]
    public class DialogChoice : IUsableComponent
    {
        public string Text { get; }
        public bool IsUsed { get; private set; }

        public void Use()
            => IsUsed = true;

        public DialogChoice(string choiceText)
            => Text = choiceText ?? throw new ArgumentException("ChoiceText can't be null");
    }
}