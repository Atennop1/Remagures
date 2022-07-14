using System.Collections.Generic;

public interface IUpgradableItem : IBaseItemComponent
{
    public int ThisItemLevel { get; }
    public int CostForThisItem { get; }
    public List<BaseInventoryItem> ItemsForLevels { get; }
}
