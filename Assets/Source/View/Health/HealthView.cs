using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Health
{
    public sealed class HealthView : SerializedMonoBehaviour, IHealthView
    {
        [SerializeField] private Image[] _hearts;
        
        [Space]
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _threeQuartersHeartSprite;
        [SerializeField] private Sprite _halfHeartSprite;
        [SerializeField] private Sprite _oneQuarterHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;

        public void Display(int value, int maxValue)
        {
            for (var i = 0; i < maxValue / 4; i++)
            {
                _hearts[i].gameObject.SetActive(true);
                _hearts[i].sprite = _fullHeartSprite;
                
                switch (value)
                {
                    case >= 4:
                        _hearts[i].sprite = _fullHeartSprite;
                        value -= 4;
                        break;
                    case 3:
                        _hearts[i].sprite = _threeQuartersHeartSprite;
                        value -= 3;
                        break;
                    case 2:
                        _hearts[i].sprite = _halfHeartSprite;
                        value -= 2;
                        break;
                    case 1:
                        _hearts[i].sprite = _oneQuarterHeartSprite;
                        value -= 1;
                        break;
                    default:
                        _hearts[i].sprite = _emptyHeartSprite;
                        break;
                }
            }
        }
    }
}
