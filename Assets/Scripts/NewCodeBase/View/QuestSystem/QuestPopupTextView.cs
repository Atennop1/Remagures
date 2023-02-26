using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.QuestSystem
{
    public class QuestPopupTextView : MonoBehaviour
    {
        [SerializeField] private Text _popupText;
        [SerializeField] private Animator _textAnimator;
        
        private readonly int SHOW_ANIMATOR_NAME = Animator.StringToHash("Show");
        
        public void DisplayText(string text)
        {
            _popupText.text = text;
            _textAnimator.SetTrigger(SHOW_ANIMATOR_NAME);
        }
    }
}
