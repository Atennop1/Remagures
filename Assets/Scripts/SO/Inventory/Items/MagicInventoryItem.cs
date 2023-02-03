using System.Collections.Generic;
using Remagures.Components;
using Remagures.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Remagures.SO
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/MagicItem")]
    public class MagicInventoryItem : BaseInventoryItem, IWeaponItem, IMagicItem, IChoiceableItem
    {
        [field: SerializeField, Header("Magic Stuff")] public bool IsCurrent { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public Projectile Projectile { get; private set; }
        [field: SerializeField] public UnityEvent UsingEvent { get; private set; }

        public void SelectIn(IEnumerable<IReadOnlyCell> inventory)
        {
            foreach (var cell in inventory)
                if (cell.Item is IChoiceableItem choiceableItem and IMagicItem)
                    choiceableItem.DisableIsCurrent();

            IsCurrent = true;
        }

        public void DisableIsCurrent()
        {
            IsCurrent = false;
        }
    }
}
