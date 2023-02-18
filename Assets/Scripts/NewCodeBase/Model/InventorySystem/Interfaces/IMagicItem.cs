namespace Remagures.Model.InventorySystem
{
    public interface IMagicItem : IUsableItem
    {
        int UsingCooldownInMilliseconds { get; }
    }
}