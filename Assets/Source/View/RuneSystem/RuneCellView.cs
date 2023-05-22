using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.RuneSystem
{
    public sealed class RuneCellView : SerializedMonoBehaviour
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
            if (_runesInventory.Cells.Any(cell => cell.Item.Equals(_runeItem)))
            {
                Display();
                return;
            }
            
            UnDisplay();
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
