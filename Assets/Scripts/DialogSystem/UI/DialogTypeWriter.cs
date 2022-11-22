using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.UI
{
    public class DialogTypeWriter : MonoBehaviour
    {
        [field: SerializeField] public DialogView View { get; private set; }
        [SerializeField] private Text _dialogText;

        private string _currentText;
        private UniTask _typingTask;
        
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;

        public async UniTask Type(string text)
        {
            _currentText = text;

            if (_typingTask.Status == UniTaskStatus.Pending)
                _cancellationTokenSource.Cancel();
            
            _typingTask = DisplayTextCoroutine(text);
            await _typingTask;
        }

        private async UniTask DisplayTextCoroutine(string text)
        {
            _dialogText.text = "";
            View.ContinueText.text = "Нажмите, чтобы пролистать";

            foreach (var letter in text.TakeWhile(_ => !_cancellationToken.IsCancellationRequested))
            {
                _dialogText.text += letter;
                await UniTask.Delay(40, cancellationToken: _cancellationToken);
            }
        }

        public void Tap()
        {
            if (_typingTask.Status == UniTaskStatus.Pending)
                _cancellationTokenSource.Cancel();

            _dialogText.text = _currentText;
            _cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
