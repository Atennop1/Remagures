using Remagures.DialogSystem.Core;
using Remagures.Player.Components;
using Remagures.SO.Other;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Remagures.DialogSystem.UI
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private Signal _endTalkSignal;

        [field: SerializeField, Header("Dialog Window")] public GameObject DialogBubble { get; private set; }
        [field: SerializeField] public GameObject DialogWindow;

        [Header("NPC Info Stuff")]
        [SerializeField] private Text _nameText;
        [SerializeField] private Image _speakerImage;
        [SerializeField] private Animator _layoutAnimator;

        [field: SerializeField, Header("Objects")] public DialogValue DialogValue { get; private set; }
        [field: SerializeField] public Text ContinueText { get; private set; }

        [Space]
        [SerializeField] private DialogChoicesHandler _choicesHandler;
        [FormerlySerializedAs("_playerInteracting")] [SerializeField] private PlayerInteractingHandler _playerInteractingHandler;
        [FormerlySerializedAs("_writter")] [SerializeField] private DialogTypeWriter _writer;

        public bool CanContinue { get; private set; } = true;
        public bool IsDialogEnded { get; private set; } = true;
    
        public Dialog ThisDialog { get; private set; }
        public int CurrentLine { get; private set; }

        private Button _dialogWindowButton;

        public void Start()
        {
            _dialogWindowButton = DialogWindow.GetComponent<Button>();
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
            DialogWindow.SetActive(true);
            Refresh(false);
        }

        public void Refresh(bool isPlayerReplica)
        {
            foreach (Transform child in DialogBubble.transform)
                Destroy(child.gameObject);

            DisplayTextCoroutine(ThisDialog.Lines[CurrentLine].Line);
            SetWindow(ThisDialog.Lines[CurrentLine]);

            if (isPlayerReplica)
                DialogBubble.transform.parent.gameObject.SetActive(false);
        }

        public void Answer(DialogChoiceView choiceView)
        {
            choiceView.Choice.OnChoiceEvent?.Invoke();
            if (ThisDialog.Choices[choiceView.Index].Dialog == null) return;
        
            CurrentLine = 0;
            ThisDialog = ThisDialog.Choices[choiceView.Index].Dialog;
        }

        private void SetWindow(DialogLine line)
        {
            _nameText.text = line.SpeakerName;
            _speakerImage.sprite = line.SpeakerSprite;
            _layoutAnimator.Play(line.LayoutType.ToString());
        }

        private void NextReplica()
        {
            if (CanContinue)
            {
                if (ThisDialog.Lines.Count - 1 != CurrentLine)
                    NextLine();
                else if (ThisDialog.Choices.Count == 0)
                    EndDialog();
                
                return;
            }
            
            SkipTyping();
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
            _playerInteractingHandler.DialogOnTaped();
            _dialogWindowButton.onClick.RemoveListener(NextReplica);
        }

        private void SkipTyping()
        {
            _writer.Tap();
            _choicesHandler.SetupChoices();
            CanContinue = true;
        }

        private async void DisplayTextCoroutine(string text)
        {
            CanContinue = false;

            await _writer.Type(text);
            _choicesHandler.SetupChoices();

            CanContinue = true;
        }
    }
}
