using Remagures.Model.Character;
using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.DialogSystem
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private Signal _endTalkSignal;

        [Header("Dialog Window")]
        [SerializeField] private GameObject _dialogBubble;
        [SerializeField] private GameObject _dialogWindow;
        [SerializeField] private GameObject _dialogBubbleBackground;

        [Header("NPC Info Stuff")]
        [SerializeField] private Text _nameText;
        [SerializeField] private Image _speakerImage;
        [SerializeField] private Animator _layoutAnimator;
        [SerializeField] private Text _continueText;

        [Space]
        [SerializeField] private DialogChoicesHandler _choicesHandler;
        [SerializeField] private CharacterInteractor _characterInteractor;
        [SerializeField] private DialogTypeWriter _writer;
        
        private Button _dialogWindowButton;
        private Dialog _currentDialog;
        
        public bool CanContinue { get; private set; } = true;
        public bool IsDialogEnded { get; private set; } = true;

        private void Awake()
            => _dialogWindowButton = _dialogWindow.GetComponent<Button>();

        public void Activate(Dialog dialog)
        {
            _continueText.text = "";
            IsDialogEnded = false;
            CanContinue = false;

            _dialogWindowButton.onClick.RemoveListener(NextReplica);
            _dialogWindowButton.onClick.AddListener(NextReplica);
            
            _currentDialog = dialog;
            _dialogWindow.SetActive(true);
            Refresh();
        }
        
        private void NextReplica()
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
            foreach (Transform child in _dialogBubble.transform)
                Destroy(child.gameObject);

            DisplayText(_currentDialog.CurrentLine.Text);
            SetupWindow(_currentDialog.CurrentLine.SpeakerData);
        }
        
        private void SetupWindow(DialogSpeakerData speakerData)
        {
            _nameText.text = speakerData.SpeakerName;
            _speakerImage.sprite = speakerData.SpeakerSprite.Get();
            _layoutAnimator.Play(speakerData.LayoutType.ToString());
        }

        private void NextLine()
        {
            _currentDialog.CurrentLine.End();
            _currentDialog.SwitchToNextLine();
            
            Refresh();
            _dialogBubbleBackground.SetActive(false);
        }

        private void EndDialog()
        {
            _currentDialog.CurrentLine.End();
            IsDialogEnded = true;      
            
            _endTalkSignal.Invoke();
            _characterInteractor.DialogOnTaped();
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
