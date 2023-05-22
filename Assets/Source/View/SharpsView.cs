using Remagures.Model.Wallet;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View
{
    public sealed class SharpsView : SerializedMonoBehaviour
    {
        [SerializeField] private Text _sharpsCountText;
        private IWallet _sharpsWallet;

        private void OnEnable()
            => _sharpsCountText.text = _sharpsWallet.MoneyCount.ToString();
    }
}