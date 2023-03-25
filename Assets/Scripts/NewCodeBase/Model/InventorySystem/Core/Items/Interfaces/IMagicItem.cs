using Remagures.Model.Magic;

namespace Remagures.Model.InventorySystem
{
    public interface IMagicItem : IUsableItem
    {
        int UsingCooldownInMilliseconds { get; }
        IMagic Magic { get; }
    }
}