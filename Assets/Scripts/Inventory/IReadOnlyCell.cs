using Remagures.SO;

namespace Remagures.Inventory
{
    public interface IReadOnlyCell
    {
        public int ItemCount { get; }
        public BaseInventoryItem Item { get; }

        public bool CanMergeWithItem(BaseInventoryItem item);
        public bool CanAddItemAmount();
        public bool ItemCountGreaterOrEqualValue(int value);
    }
}