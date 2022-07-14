using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Items/RuneItem", fileName="New Rune")]
public class RuneInventoryItem : BaseInventoryItem, IRuneItem, IChoiceableItem
{
    [field: SerializeField, Header("Rune Stuff")] public RuneType RuneType { get; private set; }
    [field: SerializeField] public ClassStat ClassStat { get; private set; }
    [field: SerializeField] public bool IsCurrent { get; private set; }


    public void SetIsCurrent(IEnumerable<IReadOnlyCell> inventory)
     {
         foreach (IReadOnlyCell cell in inventory)
            if (cell.Item is IChoiceableItem && cell.Item is IRuneItem)
                (cell.Item as IChoiceableItem).DisableIsCurrent();
                
        IsCurrent = true;
    }

    public void DisableIsCurrent()
    {
        IsCurrent = false;
    }
}
