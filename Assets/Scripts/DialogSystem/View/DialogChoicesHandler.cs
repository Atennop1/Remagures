using Remagures.DialogSystem.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.View
{
    public class DialogChoicesHandler : MonoBehaviour
    {
        [SerializeField] private DialogView _view;
        [SerializeField] private GameObject _choicePrefab;

        public void SetupChoices(Dialog dialog)
        {
            if (dialog.IsCurrentLineLast && dialog.CurrentLine.Choices.Count > 0)
            {
                _view.ContinueText.text = "";
                _view.DialogBubble.transform.parent.gameObject.SetActive(true);
                CreateChoices(dialog);
            }
            else
            {
                _view.DialogBubble.transform.parent.gameObject.SetActive(false);
                _view.ContinueText.text = "Нажмите, чтобы продолжить";
            }
        }

        private void CreateChoices(IDialog dialog)
        {
            foreach (var dialogChoice in dialog.CurrentLine.Choices)
            {
                var choiceObject = Instantiate(_choicePrefab, transform.position, Quaternion.identity, _view.DialogBubble.transform);
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
            _view.DialogBubble.transform.parent.gameObject.SetActive(false);
        }
    }
}
