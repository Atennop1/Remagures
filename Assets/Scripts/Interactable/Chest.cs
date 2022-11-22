using Remagures.DialogSystem.Runtime;
using Remagures.Interactable.Abstraction;
using Remagures.Inventory;
using Remagures.QuestSystem;
using Remagures.SO.Inventory;
using Remagures.SO.Inventory.Items;
using Remagures.SO.Other;
using Remagures.SO.PlayerStuff;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.Interactable
{
    public class Chest : InteractableWithTextDisplay
    {
        [Header("Values")]
        [SerializeField] private ItemValue _currentItem;
        [SerializeField] private FloatValue _numberOfKeys;
        [SerializeField] private BaseInventoryItem _contentsItem;

        [Space]
        [SerializeField] private PlayerInventory _inventory;
        [SerializeField] private BoolValue _storedOpened;
        [SerializeField] private Signal _raiseItemSignal;

        [FormerlySerializedAs("_changeDialogState")]
        [Header("Objects")]
        [SerializeField] private DialogStateChanger _dialogStateChanger;
        [SerializeField] private GoalCompleter _goalCompleter;

        [Space]
        [SerializeField] private Collider2D _triggerCollider;
        [SerializeField] private Animator _animator;
    
        private readonly int OPENED_ANIMATOR_NAME = Animator.StringToHash("opened");

        private void Start()
        {
            if (!_storedOpened.Value) return;
        
            _animator.SetBool(OPENED_ANIMATOR_NAME, true);
            _triggerCollider.enabled = false;
        }
    
        public override void Interact()
        {
            if (_storedOpened.Value || !PlayerInRange) return;
        
            _triggerCollider.enabled = false;
            _animator.SetBool(OPENED_ANIMATOR_NAME, true);
            _currentItem.Value = _contentsItem;

            if (_currentItem.Value is KeyInventoryItem)
            {
                _numberOfKeys.Value++;
            }
            else if (_contentsItem != null)
            {
                var newCell = new Cell(_contentsItem);
                var inventoryCell = _inventory.GetCell(newCell.Item);
                
                if (inventoryCell == null || (inventoryCell.CanAddItemAmount() && inventoryCell.CanMergeWithItem(newCell.Item)))
                    _inventory.Add(newCell);
            }

            TextDisplay(_contentsItem.ItemDescription);

            _storedOpened.Value = true;
            _raiseItemSignal.Invoke();
            
            _dialogStateChanger?.ChangeDatabaseState();
            _goalCompleter?.Complete();
        }
    }
}
