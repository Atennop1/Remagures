using System;
using Remagures.Model.Wallet;
using Remagures.Tools;

namespace Remagures.Model.Pickup
{
    public class AddMoneyPickupable : IPickupable
    {
        private readonly IWallet _wallet;
        private readonly int _amount;

        public AddMoneyPickupable(IWallet moneyWallet, int amount)
        {
            _amount = amount.ThrowExceptionIfLessOrEqualsZero();
            _wallet = moneyWallet ?? throw new ArgumentNullException(nameof(moneyWallet));
        }

        public void Pickup()
            => _wallet.Put(_amount);
    }
}
