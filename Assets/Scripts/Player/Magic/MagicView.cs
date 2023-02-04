using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Player
{
    public class MagicView : MonoBehaviour
    {
        [field: SerializeField] public Slider MagicSlider { get; private set; }
        [SerializeField] private FloatValue _maxMagic;

        public void Awake()
            => MagicSlider.maxValue = _maxMagic.Value;

        public void UpdateValue(float value)
            => MagicSlider.value = value;
    }
}
