using System.Collections.Generic;

namespace Remagures.Inventory
{
    public interface IChoiceableItem : IBaseItemComponent
    {
        public bool IsCurrent { get; }
        public void SelectIn(IEnumerable<IReadOnlyCell> inventory);
        public void DisableIsCurrent();
    }
}
