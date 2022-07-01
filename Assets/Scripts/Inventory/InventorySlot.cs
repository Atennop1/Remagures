using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [field: SerializeField, Header("UI")] protected Image ItemImage { get; private set; }
    [field: SerializeField] protected Sprite NoneSprite { get; private set; }
    [field: SerializeField] protected Text ItemCount { get; private set; }

    public BaseInventoryItem ThisItem { get; private set; }
    private InventoryView _thisManager;

    public void Setup(BaseInventoryItem newItem, InventoryView newManager)
    {
        ThisItem = newItem;
        _thisManager = newManager;
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
        _thisManager.SetText(ThisItem);
    }
    
    public void ChoiceMagicItem()
    {
        _thisManager.SetText(ThisItem);
        _thisManager.UseButton.SetActive(true);
    }
}
