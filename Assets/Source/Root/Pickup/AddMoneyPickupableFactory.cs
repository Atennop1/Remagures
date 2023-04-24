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

        public IPickupable Create()
            => new AddMoneyPickupable(_walletFactory.Create(), _amount);
    }
}