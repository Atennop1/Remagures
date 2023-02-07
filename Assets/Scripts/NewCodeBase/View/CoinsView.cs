using System.Globalization;
using Remagures.SO;
using TMPro;
using UnityEngine;

namespace Remagures.View
{
    public sealed class CoinsView : MonoBehaviour
    {
        [SerializeField] private FloatValue _numberOfCoins;
        [SerializeField] private TextMeshProUGUI _coinDisplay;

        public void Start()
            => _coinDisplay.text = _numberOfCoins.Value.ToString(CultureInfo.InvariantCulture);
    }
}
