using Remagures.Model.Wallet;
using Remagures.View.Wallet;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Wallet
{
    public sealed class WalletFactory : SerializedMonoBehaviour, IWalletFactory
    {
        [SerializeField] private IWalletView _walletView;
        private IWallet _builtWallet;
        
        public IWallet Create()
        {
            if (_builtWallet != null)
                return _builtWallet;

            _builtWallet = new Model.Wallet.Wallet(_walletView);
            return _builtWallet;
        }
    }
}