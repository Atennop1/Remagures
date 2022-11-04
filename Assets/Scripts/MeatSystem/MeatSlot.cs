using UnityEngine;
using UnityEngine.UI;

namespace Remagures.MeatSystem
{
    public class MeatSlot : MonoBehaviour
    {
        [SerializeField] private Sprite _meatNone;
        [SerializeField] private Sprite _meatSprite;
    
        [Space]
        [SerializeField] private Text _countText;
        [SerializeField] private Image _spriteRenderer;

        public void Setup(int count)
        {
            switch (count)
            {
                case > 1:
                    _countText.text = count.ToString();
                    _spriteRenderer.sprite = _meatSprite;
                    break;
                case 1:
                    _countText.text = "";
                    _spriteRenderer.sprite = _meatSprite;
                    break;
                default:
                    _countText.text = "";
                    _spriteRenderer.sprite = _meatNone;
                    break;
            }
        }
    }
}
