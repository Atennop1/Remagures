using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.DialogSystem.UI
{
    public class DialogTypeWriter : MonoBehaviour
    {
        [field: SerializeField] public DialogView View { get; private set; }
        [SerializeField] private Text _dialogText;

        private string _currentText;
        private Coroutine _typingCoroutine;

        public IEnumerator Type(string text)
        {
            _currentText = text;

            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);
            _typingCoroutine = StartCoroutine(DisplayTextCoroutine(text));

            yield return _typingCoroutine;
        }

        private IEnumerator DisplayTextCoroutine(string text)
        {
            View.ContinueText.text = "Нажмите, чтобы пролистать";
            _dialogText.text = "";

            foreach(var letter in text)
            {
                _dialogText.text += letter;
                yield return new WaitForSeconds(0.04f);
            }

            _typingCoroutine = null;
        }

        public void Tap()
        {
            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);

            _dialogText.text = _currentText;
            _typingCoroutine = null;
        }
    }
}
