using System;
using SaveSystem;

namespace Remagures.Model.Wallet
{
    public sealed class WalletWithSaving : IWallet
    {
        public int MoneyCount { get; private set; }
        
        private readonly IWallet _wallet;
        private readonly ISaveStorage<int> _saveStorage;

        public WalletWithSaving(IWallet wallet, ISaveStorage<int> saveStorage)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _saveStorage = saveStorage ?? throw new ArgumentNullException(nameof(saveStorage));
            MoneyCount = _saveStorage.HasSave() ? _saveStorage.Load() : 0;
        }

        public void Put(int count)
        {
            _wallet.Put(count);
            _saveStorage.Save(_wallet.MoneyCount);
            MoneyCount = _wallet.MoneyCount;
        }

        public void Take(int count)
        {
            _wallet.Take(count);
            _saveStorage.Save(_wallet.MoneyCount);
            MoneyCount = _wallet.MoneyCount;
        }

        public bool CanTake(int count) 
            => _wallet.CanTake(count);
    }
}