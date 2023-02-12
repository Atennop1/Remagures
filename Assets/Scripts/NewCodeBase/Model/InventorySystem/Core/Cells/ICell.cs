namespace Remagures.Model.InventorySystem
{
    public interface ICell<T> : IReadOnlyCell<T> where T: IItem
    {
        void Merge(ICell<T> anotherCell);
        void DecreaseAmount(int amount);
    }
}