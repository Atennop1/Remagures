using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.MeatSystem
{
    public sealed class MeatSlotView : SerializedMonoBehaviour, IMeatSlotView
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _countText;

        [SerializeField] private Sprite _meatAbsenceSprite;
        [SerializeField] private Sprite _meatSprite;

        public void Display(int count)
        {
            _countText.text = count >= 0 ? count.ToString() : string.Empty;
            _image.sprite = count > 0 ? _meatSprite : _meatAbsenceSprite;
        }
    }
}
