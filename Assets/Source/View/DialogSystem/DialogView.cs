using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public sealed class DialogView : SerializedMonoBehaviour, IDialogView
    {
        [SerializeField] private GameObject _dialogBubble;
        [SerializeField] private GameObject _dialogWindow;
        [SerializeField] private IDialogChoicesView _choicesView;

        [Header("NPC Info Stuff")]
        [SerializeField] private Text _nameText;
        [SerializeField] private Image _speakerImage;
        [SerializeField] private Animator _layoutAnimator;
        [SerializeField] private Text _continueText;

        public void DisplayStartOfDialog()
        {
            _continueText.text = "";
            _dialogWindow.SetActive(true);
        }

        public void DisplayEndOfDialog()
            => _dialogWindow.SetActive(false);

        public void DisplayLineInfo(IDialogLine line)
        {
            _nameText.text = line.SpeakerData.SpeakerName;
            _speakerImage.sprite = line.SpeakerData.SpeakerSprite.Get();
            _layoutAnimator.Play(line.SpeakerData.LayoutType.ToString());
        }

        public void DisplayChoices(IDialogLine line)
        {
            foreach (Transform child in _dialogBubble.transform)
                Destroy(child.gameObject);

            _choicesView.Display(line);
        }
    }
}
