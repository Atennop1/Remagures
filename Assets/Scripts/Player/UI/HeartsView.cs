using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Player
{
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
            => UpdateHearts();

        private void InitHearts()
        {
            for (var i = 0; i < _heartContainers.Value; i++)
            {
                if (i >= _hearts.Length) continue;
            
                _hearts[i].gameObject.SetActive(true);
                _hearts[i].sprite = _fullHeart;
            }
        }

        public void UpdateHearts()
        {
            InitHearts();
            var currentHearts = _currentHealth.Value;
        
            for (var i = 0; i < _heartContainers.Value; i++)
            {
                if (i >= _hearts.Length) continue;

                switch (currentHearts)
                {
                    case >= 4:
                        _hearts[i].sprite = _fullHeart;
                        currentHearts -= 4;
                        break;
                    case 3:
                        _hearts[i].sprite = _threeQuarterHeart;
                        currentHearts -= 3;
                        break;
                    case 2:
                        _hearts[i].sprite = _halfHeart;
                        currentHearts -= 2;
                        break;
                    case 1:
                        _hearts[i].sprite = _oneQuarterHeart;
                        currentHearts -= 1;
                        break;
                    default:
                        _hearts[i].sprite = _emptyHeart;
                        break;
                }
            }
        }
    }
}
