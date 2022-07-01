using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private InventorySlot _currentRuneSlot;
    [SerializeField] private MagicCounter _magicManager;
    [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    
    private RuneInventoryItem _currentRune;

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

    public void Select(RuneInventoryItem item)
    {
        _nameText.text = item.ItemData.ItemName;
        _descriptionText.text = item.ItemData.ItemDescription;
        _currentRuneImage.sprite = item.ItemData.ItemSprite;

        _equipButton.SetActive(true);

        _currentRune = item;
    }

    public void Equip()
    {
        _currentRune.RuneItemData.SetIsCurrent(Inventory.MyInventory);
        _currentRuneSlot.Setup(_currentRune, null);

        _currentRune.RuneItemData.ClassStat.ClearRunes();
        _currentRune.RuneItemData.ClassStat.SetupRunes(_currentRune, _magicManager);

        Close();
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
