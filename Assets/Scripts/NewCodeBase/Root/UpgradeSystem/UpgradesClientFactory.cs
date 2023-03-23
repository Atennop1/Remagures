using System.Collections.Generic;
using Remagures.Model.UpgradeSystem;
using Remagures.Model.Wallet;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UpgradesClientFactory : SerializedMonoBehaviour //TODO make root for wallet and little rewrite this 
    {
        [SerializeField] private Dictionary<Currency, IWallet> _wallets;
        private IUpgradesClient _builtClient;
        
        public IUpgradesClient Create()
        {
            if (_builtClient != null)
                return _builtClient;

            _builtClient = new UpgradesClient(_wallets);
            return _builtClient;
        }
    }
}