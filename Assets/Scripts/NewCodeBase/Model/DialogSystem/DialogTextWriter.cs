using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Remagures.View.DialogSystem;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public sealed class DialogTextWriter
    {
        public bool IsTyping { get; private set; }

        private readonly DialogTextWriterView _dialogTextWriterView;
        
        private string _currentText;
        private UniTask _typingTask;

        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;

        public DialogTextWriter(DialogTextWriterView dialogTextWriterView)
            => _dialogTextWriterView = dialogTextWriterView ?? throw new ArgumentNullException(nameof(dialogTextWriterView));

        public async UniTask StartTyping(string text)
        {
            _currentText = text;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _typingTask = DisplayTextCoroutine(text);
            await _typingTask;
        }
        
        public void EndTyping()
        {
            if (_typingTask.Status == UniTaskStatus.Pending)
                _cancellationTokenSource.Cancel();

            _dialogTextWriterView.DisplayText(_currentText);
            IsTyping = false;
        }

        private async UniTask DisplayTextCoroutine(string text)
        {
            IsTyping = true;
            var writingText = string.Empty;
            _dialogTextWriterView.DisplayStartOfTyping();

            foreach (var letter in text.TakeWhile(_ => !_cancellationToken.IsCancellationRequested))
            {
                writingText += letter;
                _dialogTextWriterView.DisplayText(writingText);
                await UniTask.Delay(40);
            }
            
            _dialogTextWriterView.DisplayEndOfTyping();
            IsTyping = false;
        }
    }
}
