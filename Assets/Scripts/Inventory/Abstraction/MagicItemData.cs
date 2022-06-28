using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MagicItemData
{
    [field: SerializeField] public bool IsCurrent { get; private set; }
    [field: SerializeField] public GameObject Projectile { get; private set; }
    [field: SerializeField] public UnityEvent ThisEvent { get; private set; }

    public void SetIsCurrent(IEnumerable<BaseInventoryItem> inventory)
    {
        foreach (BaseInventoryItem item in inventory)
            if ((item as MagicInventoryItem != null))
                (item as MagicInventoryItem).MagicItemData.DisableIsCurrent();

        IsCurrent = true;
    }

    public void DisableIsCurrent()
    {
        IsCurrent = false;
    }
}
