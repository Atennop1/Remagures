using Remagures.DialogSystem.Model.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.View
{
    public class DialogChoiceView : MonoBehaviour
    {
        [SerializeField] private Text _choiceText;
        public DialogChoice Choice { get; private set; }

        public void Setup(DialogChoice choice)
        {
            _choiceText.text = choice.ChoiceText;
            Choice = choice;
        }
    }
}
