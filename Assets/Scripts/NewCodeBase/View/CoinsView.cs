using System;
using System.Globalization;
using Remagures.Model.Wallet;
using TMPro;
using UnityEngine;

namespace Remagures.View
{
    public sealed class CoinsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsText;
        private IWallet _coinsWallet;

        public void Construct(IWallet coinsWallet)
            => _coinsWallet = coinsWallet ?? throw new ArgumentNullException(nameof(coinsWallet));

        public void Start()
            => _coinsText.text = _coinsWallet.MoneyCount.ToString(CultureInfo.InvariantCulture);
    }
}
