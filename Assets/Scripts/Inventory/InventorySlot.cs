using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [field: SerializeField, Header("UI")] protected Image ItemImage { get; private set; }
    [field: SerializeField] protected Sprite NoneSprite { get; private set; }
    [field: SerializeField] protected Text ItemCount { get; private set; }

    public BaseInventoryItem ThisItem { get; private set; }
    private InventoryView _thisView;

    public void Setup(BaseInventoryItem newItem, InventoryView newView)
    {
        ThisItem = newItem;
        _thisView = newView;
        if (ThisItem)
        {
            if (ThisItem.ItemData.ItemDescription != "" && ThisItem.ItemData.ItemName != "")
                ItemImage.sprite = ThisItem.ItemData.ItemSprite;
            else
                ItemImage.sprite = NoneSprite;
            if (ThisItem.ItemData.NumberHeld > 1)
                ItemCount.text = ThisItem.ItemData.NumberHeld.ToString();
            else
                ItemCount.text = "";
        }
    }

    public void Choice()
    {
        _thisView.SetText(ThisItem);
    }
    
    public void ChoiceMagicItem()
    {
        _thisView.SetText(ThisItem);
        _thisView.UseButton.SetActive(true);
    }
}
