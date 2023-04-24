using Remagures.Tools;

namespace Remagures.Model.InventorySystem
{
    public readonly struct CellSavingData
    {
        public readonly int ItemID;
        public readonly int ItemsCount;

        public CellSavingData(int itemID, int itemsCount)
        {
            ItemID = itemID.ThrowExceptionIfLessThanZero();
            ItemsCount = itemsCount.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}