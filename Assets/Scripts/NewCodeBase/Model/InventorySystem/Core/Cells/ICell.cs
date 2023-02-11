namespace Remagures.Model.InventorySystem
{
    public interface ICell : IReadOnlyCell
    {
        void Merge(ICell anotherCell);
        void DecreaseAmount(int amount);
    }
}