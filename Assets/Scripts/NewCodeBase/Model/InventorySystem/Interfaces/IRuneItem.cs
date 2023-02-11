namespace Remagures.Model.InventorySystem
{
    public interface IRuneItem : IItem
    {
        RuneType Type { get; }
    }
}