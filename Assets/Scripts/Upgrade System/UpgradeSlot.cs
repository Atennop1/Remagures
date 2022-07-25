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

    public IReadOnlyCell ThisCell { get; private set; }
    public UpgradeView View { get; private set; }
    private Player _player;


    public void Setup(IReadOnlyCell cell, UpgradeView view, Player player)
    {
        if (cell.Item != null)
        {
            View = view;
            ThisCell = cell;
            _player = player;

            _itemImage.sprite = cell.Item.ItemSprite;
            _itemName.text = cell.Item.ItemName;

            IUpgradableItem currentItem = ThisCell.Item as IUpgradableItem;
            _costText.text = currentItem.CostForThisItem.ToString();
        }
    }

    public void Buy()
    {
        IUpgradableItem currentItem = ThisCell.Item as IUpgradableItem;
        View.Inventory.Remove(new Cell(currentItem.ItemsForLevels[currentItem.ThisItemLevel-2], 1));
        View.Inventory.Add(new Cell(currentItem as BaseInventoryItem, 1));

        _player.UniqueSetup.SetUnique(_player);

        _sharps.Value -= currentItem.CostForThisItem;
        View.SharpsCountText.text = _sharps.Value.ToString(); 
        View.OnEnable();
    }
}
