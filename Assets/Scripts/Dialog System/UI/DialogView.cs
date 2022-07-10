using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    [SerializeField] private Signal _endTalkSignal;

    [field: SerializeField, Header("Dialog Window")] public GameObject DialogBubble { get; private set; }
    [SerializeField] private GameObject _dialogWindow;

    [Header("NPC Info Stuff")]
    [SerializeField] private Text _nameText;
    [SerializeField] private Image _speakerImage;
    [SerializeField] private Animator _layoutAnimator;

    [field: SerializeField, Header("Objects")] public DialogValue DialogValue { get; private set; }
    [field: SerializeField] public Text ContinueText { get; private set; }

    [Space]
    [SerializeField] private DialogChoicesHandler _choicesHandler;
    [SerializeField] private PlayerInteracting _playerInteracting;
    [SerializeField] private DialogTypeWritter _writter;

    public bool CanContinue { get; private set; } = true;
    public bool IsDialogEnded { get; private set; } = true;
    
    public Dialog ThisDialog { get; private set; }
    public int CurrentLine { get; private set; }

    private Button _dialogWindowButton;

    public void Start()
    {
        _dialogWindowButton = _dialogWindow.GetComponent<Button>();
    }

    public void Activate()
    {
        CurrentLine = 0;
        ContinueText.text = "";
        IsDialogEnded = false;
        CanContinue = false;
        
        DialogValue.ThisDialog = DialogValue.NPCDatabase.CurrentDialog;
        ThisDialog = DialogValue.ThisDialog;

        _dialogWindowButton.onClick.AddListener(NextReplica);
        _dialogWindow.SetActive(true);
        Refresh(false);
    }

    public void Refresh(bool isPlayerReplica)
    {
        foreach (Transform child in DialogBubble.transform)
            Destroy(child.gameObject);

        StartCoroutine(DisplayTextCoroutine(ThisDialog.Lines[CurrentLine].Line));
        SetWindow(ThisDialog.Lines[CurrentLine]);

        if (isPlayerReplica)
            DialogBubble.transform.parent.gameObject.SetActive(false);
    }

    public void Answer(DialogChoiceView choiceView)
    {
        choiceView.Choice.OnChoiceEvent?.Invoke();

        if (ThisDialog.Choices[choiceView.Index].Dialog != null)
        {
            CurrentLine = 0;
            ThisDialog = ThisDialog.Choices[choiceView.Index].Dialog;
        }
    }

    private void SetWindow(DialogLine line)
    {
        _nameText.text = line.SpeakerName;
        _speakerImage.sprite = line.SpeakerSprite;
        _layoutAnimator.Play(line.LayoutType.ToString());
    }

    private void NextReplica()
    {
        if (!CanContinue)
            SkipTyping();
        else
        {
            if (ThisDialog.Lines.Count - 1 != CurrentLine)
                NextLine();
            else if (ThisDialog.Choices.Count == 0)
                EndDialog();
        }
    }

    private void NextLine()
    {
        ThisDialog.Lines[CurrentLine].OnLineEnded?.Invoke();
        CurrentLine++;
        Refresh(true);
    }

    private void EndDialog()
    {
        ThisDialog.Lines[CurrentLine].OnLineEnded?.Invoke();
        IsDialogEnded = true;        
        _endTalkSignal.Invoke();
        _playerInteracting.DialogOnTaped();
        _dialogWindowButton.onClick.RemoveListener(NextReplica);
    }

    private void SkipTyping()
    {
        _writter.NextReplica();
        _choicesHandler.SetupChoices();
        CanContinue = true;
    }

    private IEnumerator DisplayTextCoroutine(string text)
    {
        CanContinue = false;

        yield return StartCoroutine(_writter.Type(text));
        _choicesHandler.SetupChoices();

        CanContinue = true;
    }
}
