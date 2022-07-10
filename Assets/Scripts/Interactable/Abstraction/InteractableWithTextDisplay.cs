using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableWithTextDisplay : Interactable
{
    [Header("Text Display Stuff")]
    [SerializeField] private Text _continueText;
    [SerializeField] private Text _nameText;

    [Space]
    [SerializeField] private Image _photoImage;
    [SerializeField] private Sprite _linkSprite;

    [Space]
    [SerializeField] private DialogTypeWritter _writter;
    [SerializeField] private Animator _layoutAnimator;
    [SerializeField] private Button _dialogButton;

    public bool CanContinue { get; private set; } = true;

    public void TextDisplay(string text)
    {
        _dialogButton.gameObject.SetActive(true);

        _nameText.text = "Линк";
        _layoutAnimator.Play("Right");
        _photoImage.sprite = _linkSprite;
        _dialogButton.onClick.AddListener(Tap);
        StartCoroutine(DisplayTextCoroutine(text));
    }

    public void Tap()
    {
        if (!CanContinue)
        {
            _writter.NextReplica();
            CanContinue = true;
            _continueText.text = "Нажмите, чтобы продолжить";
        }
        else
            _dialogButton.onClick.RemoveListener(Tap);
    } 

    public IEnumerator DisplayTextCoroutine(string text)
    {
        CanContinue = false;
       yield return StartCoroutine(_writter.Type(text));
        _continueText.text = "Нажмите, чтобы продолжить";
        CanContinue = true;
    }
}
