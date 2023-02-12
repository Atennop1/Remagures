using System.Collections.Generic;

namespace Remagures.Model.InventorySystem
{
    public interface IInventory<T> where T: IItem
    {
        IReadOnlyList<IReadOnlyCell<T>> Cells { get; }
        
        void Add(ICell<T> newCell);
        void Decrease(ICell<T> decreasingCell);
    }
}