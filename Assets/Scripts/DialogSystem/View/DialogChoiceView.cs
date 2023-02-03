using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem
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
