using Remagures.Model.DialogSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public class DialogChoicesView : MonoBehaviour
    {
        [SerializeField] private GameObject _choicePrefab;
        [SerializeField] private Text _continueText;
        
        [Space]
        [SerializeField] private DialogBubbleBackground _dialogBubbleBackground;
        [SerializeField] private GameObject _dialogBubble;

        public void Display(DialogLine dialogLine)
        {
            if (dialogLine.Choices.Count <= 0)
            {
                _dialogBubbleBackground.gameObject.SetActive(false);
                return;
            }

            _continueText.text = "";
            _dialogBubbleBackground.gameObject.SetActive(true);
            CreateChoices(dialogLine);
        }

        private void CreateChoices(DialogLine dialogLine)
        {
            foreach (var dialogChoice in dialogLine.Choices)
            {
                var choiceObject = Instantiate(_choicePrefab, transform.position, Quaternion.identity, _dialogBubble.transform);
                var choiceView = choiceObject.GetComponent<DialogChoiceView>();

                choiceView.Display(dialogChoice.Text);
                
                choiceView.GetComponent<Button>().onClick.AddListener(() =>
                {
                    dialogChoice.Use();
                    _dialogBubbleBackground.gameObject.SetActive(false);
                });
            }
        }
    }
}
