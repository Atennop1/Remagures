using System.Collections.Generic;
using System.Linq;
using Remagures.Model.UpgradeSystem;
using Remagures.Model.Wallet;
using Remagures.Root.Wallet;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UpgradesClientFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<Currency, IWalletFactory> _walletsFactories;
        private IUpgradesClient _builtClient;
        
        public IUpgradesClient Create()
        {
            if (_builtClient != null)
                return _builtClient;

            _builtClient = new UpgradesClient(_walletsFactories.ToDictionary(pair => pair.Key, pair => pair.Value.Create()));
            return _builtClient;
        }
    }
}