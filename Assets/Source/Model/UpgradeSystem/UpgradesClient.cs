using System;
using System.Collections.Generic;
using Remagures.Model.Wallet;

namespace Remagures.Model.UpgradeSystem
{
    public sealed class UpgradesClient : IUpgradesClient
    {
        private readonly Dictionary<Currency, IWallet> _wallets;

        public UpgradesClient(Dictionary<Currency, IWallet> wallets) 
            => _wallets = wallets ?? throw new ArgumentNullException(nameof(wallets));

        public bool CanBuy(IUpgradeBuyingData buyingData) 
            => _wallets.ContainsKey(buyingData.Currency) || _wallets[buyingData.Currency].CanTake(buyingData.Cost);

        public void Buy(IUpgradeBuyingData buyingData)
        {
            if (!CanBuy(buyingData))
                throw new InvalidOperationException("Can't buy this upgrade");
            
            _wallets[buyingData.Currency].Take(buyingData.Cost);
        }
    }
}