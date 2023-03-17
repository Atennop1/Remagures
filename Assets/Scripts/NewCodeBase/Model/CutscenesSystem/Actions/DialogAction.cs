using System;
using Remagures.Model.DialogSystem;
using UnityEngine.UI;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class DialogAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set;  }

        private readonly DialogTextWriter _writer;
        private readonly Button _dialogWindowButton;

        private readonly Text _continueText;
        private readonly string _text;

        public DialogAction(DialogTextWriter writer, Button dialogWindowButton, Text continueText, string text)
        {
            _dialogWindowButton = dialogWindowButton ? dialogWindowButton : throw new ArgumentNullException(nameof(dialogWindowButton));
            _writer = writer ? writer : throw new ArgumentNullException(nameof(writer));

            _continueText = continueText ? continueText : throw new ArgumentNullException(nameof(continueText));
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public async void Start()
        {
            IsStarted = true;
            _dialogWindowButton.gameObject.SetActive(true);
            _dialogWindowButton.onClick.AddListener(Tap);

            await _writer.StartTyping(_text);
            _continueText.text = "Нажмите, чтобы продолжить";
        }
        
        public void Finish()
            => _dialogWindowButton.onClick.RemoveListener(Tap);

        private void Tap()
        {
            if (_writer.IsTyping)
            {
                _writer.EndTyping();
                return;
            }

            IsFinished = true;
        }
    }
}