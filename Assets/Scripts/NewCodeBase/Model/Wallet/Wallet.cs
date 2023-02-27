using System;
using Remagures.Tools;

namespace Remagures.Model.Wallet
{
    public class Wallet<TCurrency> : IWallet
    {
        public int MoneyCount { get; private set; }
        private readonly StorageWithNames<IWallet, TCurrency> _storage;

        public Wallet()
        {
            _storage = new StorageWithNames<IWallet, TCurrency>();
            MoneyCount = _storage.Exist() ? _storage.Load<int>() : 0;
        }
        
        public void Put(int count)
        {
            MoneyCount += count.ThrowExceptionIfLessOrEqualsZero();
            VisualizeAndSave();
        }

        public void Take(int count)
        {
            if (count.ThrowExceptionIfLessThanZero() > MoneyCount)
                throw new ArgumentException($"Can't take {count} money from wallet where only {MoneyCount} money");

            MoneyCount -= count;
            VisualizeAndSave();
        }

        public bool CanTake(int count) 
            => count.ThrowExceptionIfLessOrEqualsZero() < MoneyCount;

        private void VisualizeAndSave()
        {
            _storage.Save(MoneyCount);
        }
    }
}