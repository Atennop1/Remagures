using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public interface IDialogChoiceView
    {
        Button Button { get; }
        void Display(string choiceText);
    }
}