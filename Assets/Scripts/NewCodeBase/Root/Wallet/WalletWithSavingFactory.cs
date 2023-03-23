using Remagures.Model.Wallet;
using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Wallet
{
    public sealed class WalletWithSavingFactory : SerializedMonoBehaviour, IWalletFactory
    {
        [SerializeField] private Currency _currency;
        [SerializeField] private IWalletFactory _walletFactory;
        private IWallet _builtWallet;
        
        public IWallet Create()
        {
            if (_builtWallet != null)
                return _builtWallet;

            var storage = new BinaryStorage<int>(new PathViaCurrency(_currency));
            _builtWallet = new WalletWithSaving(_walletFactory.Create(), storage);
            return _builtWallet;
        }
    }
}