using System;
using Remagures.Tools;
using Remagures.View.Wallet;

namespace Remagures.Model.Wallet
{
    public sealed class Wallet : IWallet
    {
        public int MoneyCount { get; private set; }
        private readonly IWalletView _walletView;

        public Wallet(IWalletView walletView) 
            => _walletView = walletView ?? throw new ArgumentNullException(nameof(walletView));

        public void Put(int count)
        {
            MoneyCount += count.ThrowExceptionIfLessOrEqualsZero();
            _walletView.Display(MoneyCount);
        }

        public void Take(int count)
        {
            if (count.ThrowExceptionIfLessThanZero() > MoneyCount)
                throw new ArgumentException($"Can't take {count} money from wallet where only {MoneyCount} money");

            MoneyCount -= count;
            _walletView.Display(MoneyCount);
        }

        public bool CanTake(int count) 
            => count.ThrowExceptionIfLessOrEqualsZero() < MoneyCount;
    }
}