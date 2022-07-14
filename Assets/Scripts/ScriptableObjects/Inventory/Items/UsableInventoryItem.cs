using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/UsableItem")]
public class UsableInventoryItem : BaseInventoryItem, IUsableItem
{
    [field: SerializeField, Header("Usable Info")] public UnityEvent ThisEvent { get; private set; }

    public void Use()
    {
        ThisEvent.Invoke();
    }
}
