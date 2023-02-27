using Remagures.Model.Wallet;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View
{
    public class SharpsView : MonoBehaviour
    {
        [SerializeField] private Text _sharpsCountText;
        private IWallet _sharpsWallet;

        private void OnEnable()
            => _sharpsCountText.text = _sharpsWallet.MoneyCount.ToString();
    }
}