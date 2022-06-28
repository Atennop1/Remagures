using System.Collections.Generic;
using UnityEngine;

public enum RuneType
{
    Fire,
    Mana,
    Shield
}

[System.Serializable]
public class RuneItemData
{
    [field: SerializeField] public bool IsCurrent { get; private set; }
    [field: SerializeField] public ClassStat ClassStat { get; private set; }
    [field: SerializeField] public RuneType RuneType { get; private set; }

    public void SetIsCurrent(IEnumerable<BaseInventoryItem> inventory)
    {
        foreach (BaseInventoryItem item in inventory)
            if ((item as RuneInventoryItem != null))
                (item as RuneInventoryItem).RuneItemData.DisableIsCurrent();
                
        IsCurrent = true;
    }

    public void DisableIsCurrent()
    {
        IsCurrent = false;
    }
}
