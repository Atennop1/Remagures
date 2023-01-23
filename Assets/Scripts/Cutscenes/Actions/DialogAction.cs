using Remagures.DialogSystem.View;
using UnityEngine.UI;

namespace Remagures.Cutscenes.Actions
{
    public class DialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set;  }

        private readonly DialogTypeWriter _writer;
        private readonly Button _dialogWindowButton;

        private readonly Text _continueText;
        private readonly string _text;

        public DialogAction(DialogTypeWriter writer, Button dialogWindowButton, Text continueText, string text)
        {
            _dialogWindowButton = dialogWindowButton;
            _continueText = continueText;
            _writer = writer;
            _text = text;
        }
        
        public async void Start()
        {
            IsStarted = true;
            _dialogWindowButton.gameObject.SetActive(true);
            _dialogWindowButton.onClick.AddListener(Tap);

            await _writer.Type(_text);
            _continueText.text = "Нажмите, чтобы продолжить";
        }
        
        public void Finish()
            => _dialogWindowButton.onClick.RemoveListener(Tap);

        private void Tap()
        {
            if (_writer.IsTyping)
            {
                _writer.Tap();
                return;
            }

            IsFinished = true;
        }
    }
}