using Remagures.DialogSystem.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.View
{
    public class DialogChoicesHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _choicePrefab;
        [SerializeField] private Text _continueText;
        
        [Space]
        [SerializeField] private DialogBubble _dialogBubble;
        [SerializeField] private GameObject _dialogBubbleBackground;

        public void SetupChoices(Dialog dialog)
        {
            if (dialog.IsCurrentLineLast && dialog.CurrentLine.Choices.Count > 0)
            {
                _continueText.text = "";
                _dialogBubbleBackground.SetActive(true);
                CreateChoices(dialog);
            }
            else
            {
                _continueText.text = "Нажмите, чтобы продолжить";
                _dialogBubbleBackground.SetActive(false);
            }
        }

        private void CreateChoices(IDialog dialog)
        {
            foreach (var dialogChoice in dialog.CurrentLine.Choices)
            {
                var choiceObject = Instantiate(_choicePrefab, transform.position, Quaternion.identity, _dialogBubble.gameObject.transform);
                var choiceView = choiceObject.GetComponent<DialogChoiceView>();

                choiceView.Setup(dialogChoice);
                choiceView.GetComponent<Button>().onClick.AddListener(() => Answer(choiceView));
            }
        }

        private void Answer(DialogChoiceView choiceView)
        {
            if (choiceView == null) 
                return;
            
            choiceView.Choice.OnChoiceAction?.Invoke();
            _dialogBubbleBackground.SetActive(false);
        }
    }
}
