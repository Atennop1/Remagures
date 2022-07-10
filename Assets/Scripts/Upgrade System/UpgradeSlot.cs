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
    [HideInInspector] public UpgradeView _view;

    public void Setup(BaseInventoryItem item, UpgradeView view)
    {
        if (item)
        {
            _view = view;
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
        _view.Inventory.Remove(currentItem.UpgradableItemData.ItemsForLevels[currentItem.UpgradableItemData.ThisItemLevel-2]);
        _view.Inventory.Add(currentItem, false);

        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

        player.ChangeArmor();
        player.UniqueView.SetUnique(player);
        player.PlayerMovement.SetDirection();
        player.PlayerAnimations.ChangeAnim("moving", false);

        _sharps.Value -= currentItem.UpgradableItemData.CostForThisItem;
        _view.SharpsCountText.text = _sharps.Value.ToString(); 
        _view.OnEnable();
    }
}
