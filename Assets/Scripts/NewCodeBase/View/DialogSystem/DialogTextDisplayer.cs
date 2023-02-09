using Remagures.Model.DialogSystem;
using Remagures.Model.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public sealed class DialogTextDisplayer //TODO fix this class after fixing dialog system
    {
        private Text _continueText;
        private Text _nameText;
        
        private Image _photoImage;
        private Sprite _linkSprite;
        
        private DialogTypeWriter _writer;
        private Animator _layoutAnimator;
        private Button _dialogButton;
        private UIActivityChanger _uiActivityChanger;

        public bool CanContinue { get; private set; } = true;

        public void TextDisplay(string text)
        {
            _dialogButton.gameObject.SetActive(true);

            _nameText.text = "Линк";
            _layoutAnimator.Play("Right");
            _photoImage.sprite = _linkSprite;
        
            _dialogButton.onClick.AddListener(Tap);
            DisplayTextTask(text);
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

        private async void DisplayTextTask(string text)
        {
            CanContinue = false;
            await _writer.Type(text);
       
            _continueText.text = "Нажмите, чтобы продолжить";
            CanContinue = true;
        }
    }
}
