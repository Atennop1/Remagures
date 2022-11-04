using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.UI
{
    public class DialogChoicesHandler : MonoBehaviour
    {
        [SerializeField] private DialogView _view;
        [SerializeField] private GameObject _choicePrefab;

        public void SetupChoices()
        {
            if (_view.ThisDialog.Lines.Count - 1 == _view.CurrentLine && _view.ThisDialog.Choices.Count > 0)
            {
                _view.ContinueText.text = "";
                _view.DialogBubble.transform.parent.gameObject.SetActive(true);
                CreateChoices();
            }
            else
            {
                _view.DialogBubble.transform.parent.gameObject.SetActive(false);
                _view.ContinueText.text = "Нажмите, чтобы продолжить";
            }
        }

        private void CreateChoices()
        {
            for (var i = 0; i < _view.ThisDialog.Choices.Count; i++)
            {
                var obj = Instantiate(_choicePrefab, transform.position, Quaternion.identity, _view.DialogBubble.transform);
                var choice = obj.GetComponent<DialogChoiceView>();

                choice.Setup(_view.ThisDialog.Choices[i], i);
                choice.GetComponent<Button>().onClick.AddListener(delegate { Answer(choice); });
            }
        }

        private void Answer(DialogChoiceView choice)
        {
            if (choice == null) return;
        
            _view.Answer(choice);
            _view.Refresh(true);
        }
    }
}
