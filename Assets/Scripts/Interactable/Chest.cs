using UnityEngine;

public class Chest : InteractableWithTextDisplay
{
    [Header("Values")]
    [SerializeField] private ItemValue _currentItem;
    [SerializeField] private FloatValue _numberOfKeys;
    [SerializeField] private BaseInventoryItem _contentsItem;

    [Space]
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private BoolValue _storedOpened;
    [SerializeField] private Signal _raiseItem;

    [Header("Objects")]
    [SerializeField] private ChangeDialogState _changeDialogState;
    [SerializeField] private Collider2D _triggerCollider;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        if (_storedOpened.Value)
        {
            _animator.SetBool("opened", true);
            _triggerCollider.enabled = false;
        }
    }
    
    public override void Interact()
    {
        if (!_storedOpened.Value && PlayerInRange)
        {
            _triggerCollider.enabled = false;
            _animator.SetBool("opened", true);
            _currentItem.Value = _contentsItem;
            bool isKey = (_currentItem.Value as KeyInventoryItem) != null;

            if (isKey)
                _numberOfKeys.Value++;
            else 
            {
                if (_currentItem && _contentsItem)
                {
                    _inventory.Add(_contentsItem, false);
                    if (_contentsItem.ItemData.Stackable)
                        _contentsItem.ItemData.NumberHeld++;
                }
            }

            TextDisplay(_contentsItem.ItemData.ItemDescription);
            if (_changeDialogState != null)
                _changeDialogState.ChangeDatabaseState();

            _raiseItem.Raise();
            _storedOpened.Value = true;
        }
    }
}
