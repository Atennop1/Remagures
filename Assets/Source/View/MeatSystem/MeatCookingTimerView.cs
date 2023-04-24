using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.MeatSystem
{
    public sealed class MeatCookingTimerView : MonoBehaviour
    {
        [SerializeField] private Slider _readySlider;
        [SerializeField] private Text _timeText;
        
        private void Awake()
            => _timeText.text = "0:00";

        public void DisplayTimer(float remainingTime)
        {
            _timeText.text = $"{remainingTime / 60}:{(remainingTime % 60).ToString().PadLeft(2, '0')}";
            _readySlider.value = (300 - remainingTime) / 300;
        }
    }
}