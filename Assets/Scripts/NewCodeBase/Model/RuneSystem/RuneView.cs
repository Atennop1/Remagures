using System;
using Remagures.Model.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.RuneSystem
{
    public sealed class RuneView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _currentRuneImage;
        [SerializeField] private Sprite _absenceRuneSprite;

        [Header("Objects")]
        [SerializeField] private GameObject _equipButton;
        [SerializeField] private GameObject _noneText;

        private IInventory<IRuneItem> _inventory;

        public void Construct(IInventory<IRuneItem> inventory)
            => _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        
        public void DisplayInformation(IRuneItem item)
        {
            _nameText.text = item.Name;
            _descriptionText.text = item.Description;
            _currentRuneImage.sprite = item.Sprite;
            _equipButton.SetActive(true);
        }
        
        private void OnEnable()
        {
            _currentRuneImage.sprite = _absenceRuneSprite;
            _descriptionText.text = "";
            _nameText.text = "";
            _noneText.SetActive(_inventory.Cells.Count < 1);
        }

        private void Close()
            => gameObject.SetActive(false);
    }
}
