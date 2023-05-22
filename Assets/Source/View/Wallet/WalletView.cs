using System.Globalization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Remagures.View.Wallet
{
    public sealed class WalletView : SerializedMonoBehaviour, IWalletView
    {
        [SerializeField] private TextMeshProUGUI _coinsText;

        public void Display(int value)
            => _coinsText.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
