using UnityEngine;
using UnityEngine.UI;

public class HeartsView : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private FloatValue _heartContainers;
    [SerializeField] private FloatValue _currentHealth;
    [SerializeField] private Image[] _hearts;

    [Header("Hearts")]
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _threeQuarterHeart;
    [SerializeField] private Sprite _halfHeart;
    [SerializeField] private Sprite _oneQuarterHeart;
    [SerializeField] private Sprite _emptyHeart;

    private void Start()
    {
        UpdateHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < _heartContainers.Value; i++)
        {
            if (i < _hearts.Length)
            {
                _hearts[i].gameObject.SetActive(true);
                _hearts[i].sprite = _fullHeart;
            }
        }
    }

    public void UpdateHearts()
    {
        InitHearts();
        float currentHearts = _currentHealth.Value;
        for (int i = 0; i < _heartContainers.Value; i++)
        {
            if (i < _hearts.Length)
            {
                if (currentHearts >= 4)
                {
                    _hearts[i].sprite = _fullHeart;
                    currentHearts -= 4;
                }
                else if (3 == currentHearts)
                {
                    _hearts[i].sprite = _threeQuarterHeart;
                    currentHearts -= 3;
                }
                else if (2 == currentHearts)
                {
                    _hearts[i].sprite = _halfHeart;
                    currentHearts -= 2;
                }
                else if (1 == currentHearts)
                {
                    _hearts[i].sprite = _oneQuarterHeart;
                    currentHearts -= 1;
                }
                else
                    _hearts[i].sprite = _emptyHeart;
            }
        }
    }
}
