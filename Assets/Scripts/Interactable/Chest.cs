using Remagures.DialogSystem.Model;
using Remagures.Interactable.Abstraction;
using Remagures.Inventory;
using Remagures.QuestSystem;
using Remagures.SaveSystem.SwampAttack.Runtime.Tools.SaveSystem;
using Remagures.SO.Inventory;
using Remagures.SO.Inventory.Items;
using Remagures.SO.Other;
using Remagures.SO.PlayerStuff;
using UnityEngine;

namespace Remagures.Interactable
{
    public class Chest : InteractableWithTextDisplay
    {
        [Header("Values")] [SerializeField] private ItemValue _currentItem;
        [SerializeField] private FloatValue _numberOfKeys;
        [SerializeField] private BaseInventoryItem _contentsItem;
        [SerializeField] private string _name;

        [Space] [SerializeField] private PlayerInventory _inventory;
        [SerializeField] private Signal _raiseItemSignal;

        [Header("Objects")] [SerializeField] private DialogSwitcher _dialogSwitcher;
        [SerializeField] private GoalCompleter _goalCompleter;

        [Space] [SerializeField] private Collider2D _triggerCollider;
        [SerializeField] private Animator _animator;

        private readonly int OPENED_ANIMATOR_NAME = Animator.StringToHash("opened");
        private bool _isOpened;
        private BinaryStorage _storage;

        public override void Interact()
        {
            if (_isOpened || !PlayerInRange)
                return;

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

                if (inventoryCell == null ||
                    (inventoryCell.CanAddItemAmount() && inventoryCell.CanMergeWithItem(newCell.Item)))
                    _inventory.Add(newCell);
            }

            TextDisplay(_contentsItem.ItemDescription);

            _isOpened = true;
            _raiseItemSignal.Invoke();

            _dialogSwitcher?.Switch();
            _goalCompleter?.Complete();
        }

        private void Start()
        {
            _storage = new BinaryStorage();
            if (_storage.Exist(_name))
                _isOpened = _storage.Load<bool>(_name);
            
            if (!_isOpened) 
                return;

            _animator.SetBool(OPENED_ANIMATOR_NAME, true);
            _triggerCollider.enabled = false;
        }

        private void OnDisable() => _storage.Save(_isOpened, _name);
    }
}
