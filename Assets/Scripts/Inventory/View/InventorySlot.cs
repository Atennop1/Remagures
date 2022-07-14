using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [field: SerializeField, Header("UI")] protected Image ItemImage { get; private set; }
    [field: SerializeField] protected Sprite NoneSprite { get; private set; }
    [field: SerializeField] protected Text ItemCount { get; private set; }

    [field: SerializeField, Space] public BaseInventoryItem ThisItem { get; private set; }
    public IReadOnlyCell ThisCell { get; private set; }
    protected InventoryView _thisView;

    public void Setup(IReadOnlyCell newCell, InventoryView newView)
    {
        ThisCell = newCell;
        ThisItem = (BaseInventoryItem)newCell.Item;
        _thisView = newView;

        if (ThisCell != null)
        {
            if (ThisCell.Item.ItemDescription != "" && ThisCell.Item.ItemName != "")
                ItemImage.sprite = ThisCell.Item.ItemSprite;
            else
                ItemImage.sprite = NoneSprite;
            if (ThisCell.ItemCount > 1)
                ItemCount.text = ThisCell.ItemCount.ToString();
            else
                ItemCount.text = "";
        }
    }

    public void Choice()
    {
        _thisView.UseButton.SetActive(ThisCell.Item.Stackable || ThisCell.Item is IMagicItem);
        _thisView.SetText(ThisCell);
    }
}
