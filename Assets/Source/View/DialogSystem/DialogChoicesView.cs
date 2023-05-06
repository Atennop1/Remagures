using System.Collections.Generic;
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

        private readonly List<Button> _createdChoiceViewButtons;

        public void Display(IDialogLine dialogLine)
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

        private void OnDestroy() 
            => ClearContent();

        private void CreateChoices(IDialogLine dialogLine)
        {
            ClearContent();
            
            foreach (var dialogChoice in dialogLine.Choices)
            {
                var choiceObject = Instantiate(_choicePrefab, transform.position, Quaternion.identity, _dialogBubble.transform);
                var choiceView = choiceObject.GetComponent<DialogChoiceView>();

                choiceView.Display(dialogChoice.Text);
                var choiceViewButton = choiceView.GetComponent<Button>();
                
                choiceViewButton.onClick.AddListener(() =>
                {
                    dialogChoice.Use();
                    _dialogBubbleBackground.gameObject.SetActive(false);
                });
                
                _createdChoiceViewButtons.Add(choiceViewButton);
            }
        }

        private void ClearContent()
        {
            _createdChoiceViewButtons.ForEach(button => button.onClick.RemoveAllListeners());
            _createdChoiceViewButtons.Clear();
            
            for (var i = 0; i < _dialogBubble.transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}
