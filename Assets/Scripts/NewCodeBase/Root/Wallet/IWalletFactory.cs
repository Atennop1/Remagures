using Remagures.Model.Wallet;

namespace Remagures.Root.Wallet
{
    public interface IWalletFactory
    {
        IWallet Create();
    }
}