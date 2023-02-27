using System;
using Remagures.Model.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View
{
    public sealed class HeartsView : MonoBehaviour
    {
        [SerializeField] private Image[] _hearts;
        
        [Space]
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _threeQuartersHeartSprite;
        [SerializeField] private Sprite _halfHeartSprite;
        [SerializeField] private Sprite _oneQuarterHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;

        private IHealth _characterHealth;      
        private int _heartContainersCount;

        public void Construct(IHealth health)
            => _characterHealth = health ?? throw new ArgumentNullException(nameof(health));

        private void Start()
        {
            _heartContainersCount = _characterHealth.CurrentValue / 4;
            FillHearts();
        }

        private void DisplayHearts()
        {
            for (var i = 0; i < _heartContainersCount; i++)
            {
                if (i >= _hearts.Length) 
                    continue;
            
                _hearts[i].gameObject.SetActive(true);
                _hearts[i].sprite = _fullHeartSprite;
            }
        }

        private void FillHearts()
        {
            DisplayHearts();
            var currentHealth = _characterHealth.CurrentValue;
        
            for (var i = 0; i < _heartContainersCount; i++)
            {
                if (i >= _hearts.Length) continue;

                switch (currentHealth)
                {
                    case >= 4:
                        _hearts[i].sprite = _fullHeartSprite;
                        currentHealth -= 4;
                        break;
                    case 3:
                        _hearts[i].sprite = _threeQuartersHeartSprite;
                        currentHealth -= 3;
                        break;
                    case 2:
                        _hearts[i].sprite = _halfHeartSprite;
                        currentHealth -= 2;
                        break;
                    case 1:
                        _hearts[i].sprite = _oneQuarterHeartSprite;
                        currentHealth -= 1;
                        break;
                    default:
                        _hearts[i].sprite = _emptyHeartSprite;
                        break;
                }
            }
        }
    }
}
