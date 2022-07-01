using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogView : MonoBehaviour
{
    [SerializeField] private Signal _endTalkSignal;

    [Header("Dialog Window")]
    [SerializeField] private GameObject _dialogBubble;
    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private Text _dialogText;
    [SerializeField] private Text _continueText;

    [Header("NPC Info Stuff")]
    [SerializeField] private Text _nameText;
    [SerializeField] private Animator _photoAnimator;
    [SerializeField] private Animator _layoutAnimator;

    [Header("Values")]
    [SerializeField] private GameObject _choicePrefab;
    [SerializeField] private DialogValue _dialogValue;

    public bool CanContinue { get; private set; } = true;
    public bool IsDialogEnded { get; private set; } = true;

    private Story _thisStory;
    private Coroutine _typingCoroutine;
    private string _currentText;

    private const string NAME_TAG = "speaker";
    private const string IMAGE_TAG = "image";
    private const string LAYOUT_TAG = "layout";
    private const string NEXT_DIALOG_TAG = "next";

    public void Activate()
    {
        _continueText.text = "";
        IsDialogEnded = false;
        _dialogWindow.GetComponent<Button>().onClick.AddListener(NextReplica);
        _dialogValue.ThisDialog = _dialogValue.NPCDatabase.CurrentDialog;
        _dialogWindow.SetActive(true);
        SetStory();
        Refresh(false);
    }

    private void SetStory()
    {
        if (_dialogValue.ThisDialog != null)
            _thisStory = new Story(_dialogValue.ThisDialog.text);
    }

    private void CreateDialog(string text)
    {
        _currentText = text;

        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(DisplayTextCoroutine(text));
    }

    private IEnumerator DisplayTextCoroutine(string text)
    {
        CanContinue = false;
        _continueText.text = "Нажмите, чтобы пролистать";
        _dialogText.text = "";

        foreach(char letter in text)
        {
            _dialogText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }

        CanContinue = true;
        _typingCoroutine = null;
        SetupChoices();
    }

    private void SetupChoices()
    {
        if (_thisStory.currentChoices.Count > 0)
        {
            _continueText.text = "";
            _dialogBubble.transform.parent.gameObject.SetActive(true);
            CreateChoices();
        }
        else
        {
            _dialogBubble.transform.parent.gameObject.SetActive(false);
            _continueText.text = "Нажмите, чтобы продолжить";
        }
    }

    private void CreateChoices()
    {
        int count = _thisStory.currentChoices.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(_choicePrefab, transform.position, Quaternion.identity, _dialogBubble.transform);
            DialogChoice choice = obj.GetComponent<DialogChoice>();

            choice.Setup(_thisStory.currentChoices[i].text, i);
            choice.GetComponent<Button>().onClick.AddListener(delegate { Answer(choice); });
        }
    }

    private void Refresh(bool isPlayerReplica)
    {
        foreach (Transform child in _dialogBubble.transform)
            Destroy(child.gameObject);

        while (_thisStory.canContinue)
        {
            CreateDialog(_thisStory.Continue());
            SetTags(_thisStory.currentTags);
            if (isPlayerReplica)
            {
                _dialogBubble.transform.parent.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void SetTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] split = tag.Split(':');
            if (split.Length != 2)
                Debug.LogError("Wrong tag syntax!");
                
            string tagKey = split[0].Trim();
            string tagValue = split[1].Trim();

            switch (tagKey)
            {
                case NAME_TAG:
                    _nameText.text = tagValue;
                    break;

                case IMAGE_TAG:
                    _photoAnimator.Play(tagValue);
                    break;

                case LAYOUT_TAG:
                    _layoutAnimator.Play(tagValue);
                    break;

                case NEXT_DIALOG_TAG:
                    NextGraph(_dialogValue, int.Parse(tagValue));
                    break;

                default:
                    Debug.LogError("Unknown tag!");
                    break;
            }
        }
    }

    private void Answer(DialogChoice choice)
    {
        _thisStory.ChooseChoiceIndex(choice.Index);
        Refresh(true);
    }

    private void NextReplica()
    {
        if (!CanContinue)
        {
            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);

            CanContinue = true;
            _dialogText.text = _currentText;
            SetupChoices();
            return;
        }

        if (_thisStory.currentChoices.Count == 0 && CanContinue)
        {
            if (_thisStory.canContinue)
                Refresh(false);
            else
            {
                IsDialogEnded = true;        
                _endTalkSignal.Raise();
                _dialogWindow.GetComponent<Button>().onClick.RemoveListener(NextReplica);
            }
        }
    }

    public void NextGraph(DialogValue value, int idOfLink)
    {
        DialogContainer container = value.NPCDatabase.Container;
        List<DialogNodeLinkData> linkDatas = new List<DialogNodeLinkData>();

        foreach (DialogNodeLinkData link in value.NPCDatabase.Container.NodeLinks)
            if (link.BaseNodeGUID == value.NPCDatabase.CurrentNodeGUID)
                linkDatas.Add(link);
        
        value.NPCDatabase.CurrentNodeGUID = linkDatas[idOfLink].TargetNodeGUID;
    }
}
