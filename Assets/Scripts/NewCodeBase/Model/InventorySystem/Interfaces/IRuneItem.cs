using Remagures.Model.RuneSystem;

namespace Remagures.Model.InventorySystem
{
    public interface IRuneItem : IItem
    {
        IRune Rune { get; }
    }
}