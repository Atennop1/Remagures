using Remagures.DialogSystem.View;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Remagures.Interactable.Abstraction
{
    public abstract class InteractableWithTextDisplay : Interactable
    {
        [Header("Text Display Stuff")]
        [SerializeField] private Text _continueText;
        [SerializeField] private Text _nameText;

        [Space]
        [SerializeField] private Image _photoImage;
        [SerializeField] private Sprite _linkSprite;
    
        [Space]
        [FormerlySerializedAs("writer")][SerializeField] private DialogTypeWriter _writer;
        [SerializeField] private Animator _layoutAnimator;
        [SerializeField] private Button _dialogButton;
        [SerializeField] private UIActivityChanger _uiActivityChanger;

        public bool CanContinue { get; private set; } = true;

        protected void TextDisplay(string text)
        {
            _dialogButton.gameObject.SetActive(true);

            _nameText.text = "Линк";
            _layoutAnimator.Play("Right");
            _photoImage.sprite = _linkSprite;
        
            _dialogButton.onClick.AddListener(Tap);
            DisplayTextCoroutine(text);
            _uiActivityChanger.TurnOff();
        }

        private void Tap()
        {
            if (!CanContinue)
            {
                _writer.Tap();
                CanContinue = true;
                _continueText.text = "Нажмите, чтобы продолжить";
                return;
            }
        
            _dialogButton.onClick.RemoveListener(Tap);
        }

        private async void DisplayTextCoroutine(string text)
        {
            CanContinue = false;
            await _writer.Type(text);
       
            _continueText.text = "Нажмите, чтобы продолжить";
            CanContinue = true;
        }
    }
}
