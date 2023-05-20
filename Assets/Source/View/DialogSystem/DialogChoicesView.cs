using System.Collections.Generic;
using Remagures.Model.DialogSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public sealed class DialogChoicesView : MonoBehaviour, IDialogChoicesView
    {
        [SerializeField] private GameObject _choicePrefab;
        [SerializeField] private Text _continueText;
        
        [Space]
        [SerializeField] private GameObject _dialogBubbleBackgroundGameObject;
        [SerializeField] private GameObject _dialogBubble;

        private readonly List<IDialogChoiceView> _createdChoiceViews;

        public void Display(IDialogLine dialogLine)
        {
            if (dialogLine.Choices.Count <= 0)
            {
                _dialogBubbleBackgroundGameObject.SetActive(false);
                return;
            }

            _continueText.text = "";
            _dialogBubbleBackgroundGameObject.SetActive(true);
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
                var choiceView = choiceObject.GetComponent<IDialogChoiceView>();

                choiceView.Display(dialogChoice.Text);
                choiceView.Button.onClick.AddListener(() =>
                {
                    dialogChoice.Use();
                    _dialogBubbleBackgroundGameObject.SetActive(false);
                });
                
                _createdChoiceViews.Add(choiceView);
            }
        }

        private void ClearContent()
        {
            _createdChoiceViews.ForEach(view => view.Button.onClick.RemoveAllListeners());
            _createdChoiceViews.Clear();
            
            for (var i = 0; i < _dialogBubble.transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}
