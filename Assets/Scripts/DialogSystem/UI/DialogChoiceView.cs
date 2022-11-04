using Remagures.DialogSystem.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.UI
{
    public class DialogChoiceView : MonoBehaviour
    {
        [SerializeField] private Text _choiceText;

        public int Index { get; private set; }
        public DialogChoice Choice { get; private set; }

        public void Setup(DialogChoice choice, int index)
        {
            _choiceText.text = choice.ChoiceText;
            Choice = choice;
            Index = index;
        }
    }
}
