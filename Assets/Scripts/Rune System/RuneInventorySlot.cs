using UnityEngine;
using UnityEngine.UI;

public class RuneInventorySlot : InventorySlot
{
    [SerializeField] private RuneView _runeManager;

    public void OnEnable()
    {
        if (!_runeManager.Inventory.Contains(ThisItem))
        {
            ItemImage.sprite = NoneSprite;
            GetComponent<Button>().enabled = false;
        }
        else
        {
            ItemImage.sprite = ThisItem.ItemData.ItemSprite;
            GetComponent<Button>().enabled = true;
        }
    }
    
    public void Select()
    {
        _runeManager.Select(ThisItem as RuneInventoryItem);
    }
}
