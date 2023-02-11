using System.Collections.Generic;

namespace Remagures.Model.InventorySystem
{
    public interface IInventory
    {
        IReadOnlyList<IReadOnlyCell> Cells { get; }
        
        void Add(ICell newCell);
        void Decrease(ICell decreasingCell);
    }
}