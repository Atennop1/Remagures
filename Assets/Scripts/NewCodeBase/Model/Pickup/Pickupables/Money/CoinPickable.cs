using System;
using Remagures.Model.Wallet;

namespace Remagures.Model.Pickup
{
    public class CoinPickable : IPickupable
    {
        private readonly IWallet _moneyWallet;

        public CoinPickable(IWallet moneyWallet)
            => _moneyWallet = moneyWallet ?? throw new ArgumentNullException(nameof(moneyWallet));

        public void Pickup()
            => _moneyWallet.Put(1);
    }
}
