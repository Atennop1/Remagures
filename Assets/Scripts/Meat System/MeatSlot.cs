using UnityEngine;
using UnityEngine.UI;

public class MeatSlot : MonoBehaviour
{
    [SerializeField] private Sprite _meatNone;
    [SerializeField] private Sprite _meatSprite;
    
    [Space]
    [SerializeField] private Text _countText;
    [SerializeField] private Image _spriteRenderer;

    public void Setup(int count)
    {
        if (count > 1)
        {
            _countText.text = count.ToString();
            _spriteRenderer.sprite = _meatSprite;
        }
        else if (count == 1)
        {
            _countText.text = "";
            _spriteRenderer.sprite = _meatSprite;
        }
        else
        {
            _countText.text = "";
            _spriteRenderer.sprite = _meatNone;
        }
    }
}
