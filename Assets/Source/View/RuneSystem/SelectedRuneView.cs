using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.RuneSystem
{
    public sealed class SelectedRuneView : MonoBehaviour, IItemInfoView<IRuneItem>
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _currentRuneImage;
        [SerializeField] private GameObject _equipButton;

        public void Display(IRuneItem item)
        {
            _nameText.text = item.Name;
            _descriptionText.text = item.Description;
            _currentRuneImage.sprite = item.Sprite;
            _equipButton.SetActive(true);
        }
        
        private void OnEnable()
        {
            _descriptionText.text = "";
            _nameText.text = "";
        }

        private void Close()
            => gameObject.SetActive(false);
    }
}
