using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableWithTextDisplay : Interactable
{
    [Header("Text Display Stuff")]
    [SerializeField] private Text _dialogText;
    [SerializeField] private Text _continueText;
    [SerializeField] private Text _nameText;

    private string _currentText;
    private Coroutine _typingCoroutine;
    private GameObject _dialogObject;

    public bool CanCloseText { get; private set; } = true;

    public void TextDisplay(string text)
    {
        _currentText = text;
        _dialogObject = _dialogText.gameObject;
        _dialogObject.transform.parent.gameObject.SetActive(true);

        _nameText.text = "Линк";
        _nameText.transform.parent.parent.parent.GetComponent<Animator>().Play("Right");
        _dialogObject.transform.parent.GetComponent<Button>().onClick.AddListener(Tap);

        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(DisplayTextCoroutine(text));
    }

    public void Tap()
    {
        if (_typingCoroutine != null)
        {
            _dialogText.text = _currentText;
            StopCoroutine(_typingCoroutine);

            CanCloseText = true;
            _typingCoroutine = null;
            _continueText.text = "Нажмите, чтобы продолжить";
        }
        else
            _dialogObject.transform.parent.GetComponent<Button>().onClick.RemoveListener(Tap);
    } 

    public IEnumerator DisplayTextCoroutine(string text)
    {
        _continueText.text = "Нажмите, чтобы пролистать";
        CanCloseText = false;

        _dialogText.text = "";
        foreach(char letter in text)
        {
            _dialogText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
        
        _continueText.text = "Нажмите, чтобы продолжить";
        CanCloseText = true;
        _typingCoroutine = null;
    }
}
