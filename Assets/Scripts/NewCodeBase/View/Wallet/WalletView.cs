using System.Globalization;
using TMPro;
using UnityEngine;

namespace Remagures.View.Wallet
{
    public sealed class WalletView : MonoBehaviour, IWalletView
    {
        [SerializeField] private TextMeshProUGUI _coinsText;

        public void Display(int value)
            => _coinsText.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
