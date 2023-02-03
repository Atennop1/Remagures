using UnityEngine;

namespace Remagures.Inventory
{
    public class UniqueSlot : Slot
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
}
