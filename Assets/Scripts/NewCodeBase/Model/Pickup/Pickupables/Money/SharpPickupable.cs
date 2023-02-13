using System;
using Remagures.Model.Wallet;

namespace Remagures.Model.Pickup
{
    public class SharpPickupable : IPickupable
    {
        private readonly IWallet _sharpsWallet;

        public SharpPickupable(IWallet sharpsWallet)
            => _sharpsWallet = sharpsWallet ?? throw new ArgumentNullException(nameof(sharpsWallet));

        public void Pickup()
            => _sharpsWallet.Put(1);
    }
}