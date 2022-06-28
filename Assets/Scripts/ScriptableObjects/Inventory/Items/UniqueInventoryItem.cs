using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueInventoryItem : BaseInventoryItem
{
    [field: SerializeField] public UniqueItemData UniqueItemData { get; private set; } = new UniqueItemData();
}
