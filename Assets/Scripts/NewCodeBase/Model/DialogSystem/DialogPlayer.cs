using System;
using Cysharp.Threading.Tasks;
using Remagures.View.DialogSystem;

namespace Remagures.Model.DialogSystem
{
    public sealed class DialogPlayer
    {
        private readonly TextWriter _textWriter;
        private readonly DialogView _dialogView;
        
        private Dialog _currentDialog;
        private bool _canContinue;

        public DialogPlayer(TextWriter textWriter, DialogView dialogView)
        {
            _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
            _dialogView = dialogView ?? throw new ArgumentNullException(nameof(dialogView));
        }

        public async void Play(Dialog dialog)
        {
            _canContinue = false;
            _currentDialog = dialog ?? throw new ArgumentNullException(nameof(dialog));
            
            _dialogView.DisplayStartOfDialog();
            _dialogView.DisplayLineInfo(_currentDialog.CurrentLine);
            await TypeText(_currentDialog.CurrentLine.Text);
        }

        public void ContinueDialog()
        {
            if (!_canContinue)
                SkipTyping();

            if (!_currentDialog.IsCurrentLineLast) 
                PlayNextLine();
            
            if (_currentDialog.IsCurrentLineLast && _currentDialog.CurrentLine.Choices.Count == 0)
                EndDialog();
        }

        private async void PlayNextLine()
        {
            _currentDialog.CurrentLine.End();
            _currentDialog.SwitchToNextLine();

            _canContinue = false;
            _dialogView.DisplayLineInfo(_currentDialog.CurrentLine);
            await TypeText(_currentDialog.CurrentLine.Text);
        }

        private void EndDialog()
        {
            _currentDialog.CurrentLine.End();
            _dialogView.DisplayEndOfDialog();
        }

        private void SkipTyping()
        {
            _textWriter.EndTyping();
            _dialogView.DisplayLineChoices(_currentDialog.CurrentLine);
            _canContinue = true;
        }

        private async UniTask TypeText(string text)
        {
            await _textWriter.StartTyping(text);
            
            if (_canContinue)
                return;
            
            _dialogView.DisplayLineChoices(_currentDialog.CurrentLine);
            _canContinue = true;
        }
    }
}