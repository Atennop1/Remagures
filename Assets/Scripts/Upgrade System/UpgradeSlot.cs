using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _itemName;
    [SerializeField] private Text _costText;

    [Space]
    [SerializeField] private FloatValue _sharps;

    [HideInInspector] public BaseInventoryItem _thisItem;
    [HideInInspector] public UpgradeManager _manager;

    public void Setup(BaseInventoryItem item, UpgradeManager manager)
    {
        if (item)
        {
            this._manager = manager;
            _thisItem = item;
            _itemImage.sprite = item.ItemData.ItemSprite;
            _itemName.text = item.ItemData.ItemName;
            UpgradableInventoryItem currentItem = _thisItem as UpgradableInventoryItem;
            _costText.text = currentItem.UpgradableItemData.CostForThisItem.ToString();
        }
    }

    public void Buy()
    {
        UpgradableInventoryItem currentItem = _thisItem as UpgradableInventoryItem;
        _manager.Inventory.Remove(currentItem.UpgradableItemData.ItemsForLevels[currentItem.UpgradableItemData.ThisItemLevel-2]);
        _manager.Inventory.Add(currentItem, false);
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.ChangeArmor();
        player.UniqueManager.SetUnique(player);
        player.PlayerMovement.SetDirection();
        player.PlayerAnimations.ChangeAnim("moving", false);
        _sharps.Value -= currentItem.UpgradableItemData.CostForThisItem;
        _manager.SharpsCountText.text = _sharps.Value.ToString(); 
        _manager.OnEnable();
    }
}
