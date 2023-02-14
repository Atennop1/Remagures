using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Mana
{
    public sealed class ManaView : MonoBehaviour, IManaView
    {
        [SerializeField] private Slider _magicSlider;

        public void Display(int currentValue, int maxValue)
        {
            _magicSlider.maxValue = maxValue;
            _magicSlider.value = currentValue;
        }
    }
}
