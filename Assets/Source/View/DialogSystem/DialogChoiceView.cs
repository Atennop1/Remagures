using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public sealed class DialogChoiceView : MonoBehaviour, IDialogChoiceView
    {
        [field: SerializeField] public Button Button { get; private set; }
        [SerializeField] private Text _choiceText;

        public void Display(string choiceText)
            => _choiceText.text = choiceText;
    }
}
