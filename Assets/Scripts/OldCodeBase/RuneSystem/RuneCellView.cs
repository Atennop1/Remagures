using UnityEngine;
using UnityEngine.UI;

namespace Remagures.RuneSystem
{
    public sealed class RuneCellView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        [SerializeField] private Sprite _absenceRuneSprite;
        [SerializeField] private Sprite _runeSprite;

        public void Display()
        {
            _image.sprite = _runeSprite;
            _button.enabled = true;
        }

        public void UnDisplay()
        {
            _image.sprite = _absenceRuneSprite;
            _button.enabled = false;
        }
    }
}
