using Remagures.Model.Pickup;
using Remagures.Root.Wallet;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class AddMoneyPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IWalletFactory _walletFactory;
        [SerializeField] private int _amount;
        private IPickupable _builtPickupable;

        public IPickupable Create()
        {
            if (_builtPickupable != null)
                return _builtPickupable;

            _builtPickupable = new AddMoneyPickupable(_walletFactory.Create(), _amount);
            return _builtPickupable;
        }
    }
}