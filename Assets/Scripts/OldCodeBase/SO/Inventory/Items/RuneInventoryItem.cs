using System.Collections.Generic;
using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(menuName="Inventory/Items/RuneItem", fileName="New Rune")]
    public class RuneInventoryItem : BaseInventoryItem, IRuneItem, IChoiceableItem
    {
        [field: SerializeField, Header("Rune Stuff")] public RuneType RuneType { get; private set; }
        [field: SerializeField] public CharacterInfo CharacterInfo { get; private set; }
        public bool IsCurrent { get; private set; }
        
        public void SelectIn(IEnumerable<IReadOnlyCell> inventory)
        {
            foreach (var cell in inventory)
                if (cell.Item is IChoiceableItem and IRuneItem)
                    (cell.Item as IChoiceableItem)?.DisableIsCurrent();
         
            IsCurrent = true;
        }

        public void DisableIsCurrent()
            => IsCurrent = false;
    }
}
