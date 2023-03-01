using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.RuneSystem
{
    public sealed class RuneView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        [SerializeField] private Sprite _absenceRuneSprite;
        [SerializeField] private Sprite _runeSprite;

        private IInventory<IRuneItem> _runesInventory;
        private IRuneItem _runeItem;

        public void Construct(IInventory<IRuneItem> runesInventory, IRuneItem runeItem)
        {
            _runesInventory = runesInventory ?? throw new ArgumentNullException(nameof(runesInventory));
            _runeItem = runeItem ?? throw new ArgumentNullException(nameof(runeItem));
        }

        private void OnEnable()
        {
            UnDisplay();
            
            if (_runesInventory.Cells.Any(cell => cell.Item.Equals(_runeItem)))
                Display();
        }

        private void Display()
        {
            _image.sprite = _runeSprite;
            _button.enabled = true;
        }

        private void UnDisplay()
        {
            _image.sprite = _absenceRuneSprite;
            _button.enabled = false;
        }
    }
}
