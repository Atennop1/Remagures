using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/MagicItem")]
public class MagicInventoryItem : BaseInventoryItem, IWeaponItem, IMagicItem, IChoiceableItem
{
    [field: SerializeField, Header("Magic Stuff")] public bool IsCurrent { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public GameObject Projectile { get; private set; }
    [field: SerializeField] public UnityEvent ThisEvent { get; private set; }


    public void SetIsCurrent(IEnumerable<IReadOnlyCell> inventory)
     {
         foreach (IReadOnlyCell cell in inventory)
             if (cell.Item is IChoiceableItem && cell.Item is IMagicItem)
                 (cell.Item as IChoiceableItem).DisableIsCurrent();

         IsCurrent = true;
     }

     public void DisableIsCurrent()
     {
         IsCurrent = false;
     }
}
