namespace Remagures.Model.Wallet
{
    public interface IWallet
    {
        int Money { get; }
        void Put(int count);
        void Take(int count);
        bool CanTake(int count);
    }
}