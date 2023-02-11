using System.Collections.Generic;

namespace Remagures.Model.InventorySystem
{
    public interface IChoiceableItem : IBaseItemComponent
    {
        public bool IsCurrent { get; }
        public void SelectIn(IEnumerable<IReadOnlyCell> inventory);
        public void DisableIsCurrent();
    }
}
