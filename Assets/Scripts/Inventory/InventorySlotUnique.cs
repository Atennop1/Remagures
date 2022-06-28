using UnityEngine;

public enum UniqueClass
{
    Helmet,
    Chestplate,
    Leggins,
    Weapon,
    Magic,
    Rune
}
public class InventorySlotUnique : InventorySlot
{
    [Space]
    [SerializeField] private GameObject _magicMenu;
    [SerializeField] private GameObject _runeMenu;

    public void ChoiceMagicSlot(GameObject equipButton)
    {
        _magicMenu.SetActive(true);
        equipButton.SetActive(false);
    }

    public void ChoiceRune(GameObject equipButton)
    {
        _runeMenu.SetActive(true);
        equipButton.SetActive(false);
    }
}
