using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] protected GameObject _inventorySlot;
    [SerializeField] protected GameObject _inventoryPanel;

    [Header("Objects")]
    [SerializeField] private Text _noItemsText;
    [field: SerializeField] public GameObject UseButton { get; private set; }
    [SerializeField] private Text _nameText;
    [field: SerializeField] protected Text DescriptionText { get; private set; }
    [SerializeField] private Text _sharpsCountText;
    [SerializeField] private Player _player;

    [field: SerializeField, Header("Values")] protected PlayerInventory PlayerInventory { get; private set; }
    [field: SerializeField] protected BaseInventoryItem VoidItem { get; private set; }
    [SerializeField] private FloatValue _sharps;

    protected IReadOnlyCell _currentCell { get; private set; }

    public void Start()
    {
        SetText(new Cell(VoidItem));
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        ClearInventory();
        MakeInventorySlots();
        SetText(new Cell(VoidItem));
        
        if(_sharpsCountText)
            _sharpsCountText.text = _sharps.Value.ToString(); 
    }

    protected void MakeInventorySlots()
    {
        if (_noItemsText != null)
            _noItemsText.gameObject.SetActive(PlayerInventory.MyInventory.Count == 0);

        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            if (PlayerInventory.MyInventory[i].Item != null && PlayerInventory.MyInventory[i].ItemCount > 0)
            {
                GameObject slotObject = Instantiate(_inventorySlot, _inventoryPanel.transform.position, Quaternion.identity, _inventoryPanel.transform);
                if (slotObject.TryGetComponent<InventorySlot>(out InventorySlot newSlot))
                    newSlot.Setup(PlayerInventory.MyInventory[i], this);
            }
        }
    }

    public void SetText(IReadOnlyCell cell)
    {
        _currentCell = cell;
        _nameText.text = _currentCell.Item.ItemName;
        DescriptionText.text = _currentCell.Item.ItemDescription;
        SetButton();
    }

    public virtual void SetButton()
    {
        RectTransform buttonRect = DescriptionText.gameObject.GetComponent<RectTransform>();
        UseButton.SetActive(_currentCell.Item as IUsableItem != null);

        if (_currentCell.Item as IUsableItem != null)
        {
            buttonRect.anchoredPosition = new Vector3(-81.58096f, buttonRect.anchoredPosition.y, 0);
            buttonRect.sizeDelta = new Vector2(1941.4f, buttonRect.sizeDelta.y);
        }
        else
        {
            buttonRect.anchoredPosition = new Vector3(6.96f, buttonRect.anchoredPosition.y, 0);
            buttonRect.sizeDelta = new Vector2(2806.157f, buttonRect.sizeDelta.y);
        }    
    }

    private void ClearInventory()
    {
        for (int i = 0; i < _inventoryPanel.transform.childCount; i++)
            Destroy(_inventoryPanel.transform.GetChild(i).gameObject);
    }

    public void UseItem()
    {
        (_currentCell.Item as IUsableItem).Use();
        PlayerInventory.Decrease(new Cell(_currentCell.Item, 1));

        if (_currentCell.ItemCount <= 0)
        {
            PlayerInventory.Remove(new Cell(_currentCell.Item, _currentCell.ItemCount));
            SetText(new Cell(VoidItem));
        }
        
        ClearInventory();
        MakeInventorySlots();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        SetupPlayer();
    }

    public virtual void SetupPlayer()
    {
        Time.timeScale = 1;
        _player.Awake();
    }
}
