using System;
using Remagures.Tools;
using SaveSystem.Paths;

namespace Remagures.Model.Wallet
{
    public sealed class Wallet : IWallet
    {
        public int MoneyCount { get; private set; }

        public void Put(int count) 
            => MoneyCount += count.ThrowExceptionIfLessOrEqualsZero();

        public void Take(int count)
        {
            if (count.ThrowExceptionIfLessThanZero() > MoneyCount)
                throw new ArgumentException($"Can't take {count} money from wallet where only {MoneyCount} money");

            MoneyCount -= count;
        }

        public bool CanTake(int count) 
            => count.ThrowExceptionIfLessOrEqualsZero() < MoneyCount;
    }
}