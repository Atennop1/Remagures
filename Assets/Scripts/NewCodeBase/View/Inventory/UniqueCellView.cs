using UnityEngine;

namespace Remagures.View.Inventory
{
    public class UniqueCellView : CellView
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
