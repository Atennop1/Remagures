using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Remagures.View.DialogSystem;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class TextWriter : MonoBehaviour
    {
        public bool IsTyping { get; private set; }

        private readonly TextWriterView _textWriterView;
        
        private string _currentText;
        private UniTask _typingTask;

        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;

        public TextWriter(TextWriterView textWriterView)
            => _textWriterView = textWriterView ?? throw new ArgumentNullException(nameof(textWriterView));

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

            _textWriterView.DisplayText(_currentText);
            IsTyping = false;
        }

        private async UniTask DisplayTextCoroutine(string text)
        {
            IsTyping = true;
            var writingText = string.Empty;
            _textWriterView.DisplayStartOfTyping();

            foreach (var letter in text.TakeWhile(_ => !_cancellationToken.IsCancellationRequested))
            {
                writingText += letter;
                _textWriterView.DisplayText(writingText);
                await UniTask.Delay(40);
            }
            
            _textWriterView.DisplayEndOfTyping();
            IsTyping = false;
        }
    }
}
