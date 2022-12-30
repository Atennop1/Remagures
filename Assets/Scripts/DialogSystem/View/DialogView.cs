using Remagures.DialogSystem.Model;
using Remagures.DialogSystem.Model.Core;
using Remagures.Player.Components;
using Remagures.SO.Other;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Remagures.DialogSystem.View
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
        [field: SerializeField] public Text ContinueText { get; private set; }

        [Space]
        [SerializeField] private DialogChoicesHandler _choicesHandler;
        [FormerlySerializedAs("_playerInteracting")] [SerializeField] private PlayerInteractingHandler _playerInteractingHandler;
        [FormerlySerializedAs("_writter")] [SerializeField] private DialogTypeWriter _writer;
        
        private Button _dialogWindowButton;
        private Dialog _currentDialog;
        
        public bool CanContinue { get; private set; } = true;
        public bool IsDialogEnded { get; private set; } = true;

        private void Awake()
        {
            _dialogWindowButton = DialogWindow.GetComponent<Button>();
        }

        public void Activate(Dialog dialog)
        {
            ContinueText.text = "";
            IsDialogEnded = false;
            CanContinue = false;
            
            _currentDialog = dialog;
            _dialogWindowButton.onClick.AddListener(NextReplica);
            
            DialogWindow.SetActive(true);
            Refresh();
        }
        
        public void NextReplica()
        {
            if (CanContinue)
            {
                if (!_currentDialog.IsCurrentLineLast) NextLine();
                else if (_currentDialog.CurrentLine.Choices.Count == 0) EndDialog();
                return;
            }
            
            SkipTyping();
        }

        private void Refresh()
        {
            foreach (Transform child in DialogBubble.transform)
                Destroy(child.gameObject);

            DisplayText(_currentDialog.CurrentLine.Line);
            SetupWindow(_currentDialog.CurrentLine.SpeakerInfo);
        }
        
        private void SetupWindow(DialogSpeakerInfo speakerInfo)
        {
            _nameText.text = speakerInfo.SpeakerName;
            _speakerImage.sprite = speakerInfo.SpeakerSprite;
            _layoutAnimator.Play(speakerInfo.LayoutType.ToString());
        }

        private void NextLine()
        {
            _currentDialog.CurrentLine.OnLineEndedAction?.Invoke();
            _currentDialog.SwitchToNextLine();
            
            Refresh();
            DialogBubble.transform.parent.gameObject.SetActive(false);
        }

        private void EndDialog()
        {
            _currentDialog.CurrentLine.OnLineEndedAction?.Invoke();
            IsDialogEnded = true;      
            
            _endTalkSignal.Invoke();
            _playerInteractingHandler.DialogOnTaped();
            _dialogWindowButton.onClick.RemoveListener(NextReplica);
        }

        private void SkipTyping()
        {
            _writer.Tap();
            _choicesHandler.SetupChoices(_currentDialog);
            CanContinue = true;
        }

        private async void DisplayText(string text)
        {
            CanContinue = false;
            await _writer.Type(text);

            if (CanContinue)
                return;
            
            _choicesHandler.SetupChoices(_currentDialog);
            CanContinue = true;
        }
    }
}
