using System;
using Remagures.Tools;

namespace Remagures.Model.Wallet
{
    public class Wallet<TCurrency> : IWallet
    {
        public int Money { get; private set; }
        private readonly StorageWithNames<IWallet, TCurrency> _storage;

        public Wallet()
        {
            _storage = new StorageWithNames<IWallet, TCurrency>();
            Money = _storage.Exist() ? _storage.Load<int>() : 0;
        }
        
        public void Put(int count)
        {
            Money += count.ThrowExceptionIfLessOrEqualsZero();
            VisualizeAndSave();
        }

        public void Take(int count)
        {
            if (count.ThrowExceptionIfLessThanZero() > Money)
                throw new ArgumentException($"Can't take {count} money from wallet where only {Money} money");

            Money -= count;
            VisualizeAndSave();
        }

        public bool CanTake(int count) 
            => count.ThrowExceptionIfLessOrEqualsZero() < Money;

        private void VisualizeAndSave()
        {
            _storage.Save(Money);
        }
    }
}