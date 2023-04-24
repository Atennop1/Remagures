using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public class DialogChoiceView : MonoBehaviour
    {
        [SerializeField] private Text _choiceText;

        public void Display(string choiceText)
            => _choiceText.text = choiceText;
    }
}
