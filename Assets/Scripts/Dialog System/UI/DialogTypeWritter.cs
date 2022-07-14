using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTypeWritter : MonoBehaviour
{
    [SerializeField] private DialogView _view;
    [SerializeField] private Text _dialogText;

    public string CurrentText { get; private set; }
    private Coroutine _typingCoroutine;

    public void StartTyping(string text)
    {
        StartCoroutine(Type(text));
    }

    public IEnumerator Type(string text)
    {
        CurrentText = text;

        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(DisplayTextCoroutine(text));

        yield return _typingCoroutine;
    }

    private IEnumerator DisplayTextCoroutine(string text)
    {
        _view.ContinueText.text = "Нажмите, чтобы пролистать";
        _dialogText.text = "";

        foreach(char letter in text)
        {
            _dialogText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }

        _typingCoroutine = null;
    }

    public void NextReplica()
    {
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        _dialogText.text = CurrentText;
        _typingCoroutine = null;
    }
}
