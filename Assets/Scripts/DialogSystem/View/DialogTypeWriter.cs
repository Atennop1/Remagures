using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.View
{
    public class DialogTypeWriter : MonoBehaviour
    {
        [field: SerializeField] public DialogView View { get; private set; }
        [SerializeField] private Text _dialogText;

        public bool IsTyping { get; private set; }

        private string _currentText;
        private UniTask _typingTask;

        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;

        public async UniTask Type(string text)
        {
            _currentText = text;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _typingTask = DisplayTextCoroutine(text);
            await _typingTask;
        }

        private async UniTask DisplayTextCoroutine(string text)
        {
            IsTyping = true;
            _dialogText.text = "";
            View.ContinueText.text = "Нажмите, чтобы пролистать";

            foreach (var letter in text.TakeWhile(letter => !_cancellationToken.IsCancellationRequested))
            {
                _dialogText.text += letter;
                await UniTask.Delay(40);
            }

            IsTyping = false;
        }

        public void Tap()
        {
            if (_typingTask.Status == UniTaskStatus.Pending)
                _cancellationTokenSource.Cancel();

            _dialogText.text = _currentText;
            IsTyping = false;
        }

        private void Awake() => _cancellationTokenSource = new CancellationTokenSource();
    }
}
