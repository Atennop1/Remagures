using Remagures.Model.Magic;

namespace Remagures.Model.InventorySystem
{
    public interface IMagicItem : IItem
    {
        IMagic Magic { get; }
    }
}