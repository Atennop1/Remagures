using Remagures.Inventory;
using Remagures.Inventory.Abstraction;
using Remagures.Inventory.View;
using Remagures.Player.Magic;
using Remagures.SO.Inventory;
using Remagures.SO.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.RuneSystem
{
    public class RuneView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _currentRuneImage;
        [SerializeField] private Sprite _noneRune;

        [Header("Objects")]
        [SerializeField] private GameObject _equipButton;
        [SerializeField] private GameObject _noneText;
        [SerializeField] private Slot _currentRuneSlot;
        [SerializeField] private MagicCounter _magicCounter;
        [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    
        private IRuneItem _currentRune;

        public void Start()
        {
            Close();
        }

        public void OnEnable()
        {
            _currentRuneImage.sprite = _noneRune;
            _descriptionText.text = "";
            _nameText.text = "";
            _noneText.SetActive(Inventory.MyInventory.Count < 1);
        }

        public void Select(IRuneItem item)
        {
            _nameText.text = item.ItemName;
            _descriptionText.text = item.ItemDescription;
            _currentRuneImage.sprite = item.ItemSprite;

            _equipButton.SetActive(true);

            _currentRune = item;
        }

        public void Equip()
        {
            (_currentRune as IChoiceableItem)?.SelectIn(Inventory.MyInventory);
            _currentRuneSlot.Setup(new Cell((BaseInventoryItem)_currentRune), null);

            _currentRune.CharacterInfo.ClearRunes();
            _currentRune.CharacterInfo.SetupRunes(_currentRune, _magicCounter);

            Close();
        }
    
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
