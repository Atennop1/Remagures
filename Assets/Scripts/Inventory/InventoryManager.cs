using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] protected GameObject _inventorySlot;
    [SerializeField] protected GameObject _inventoryPanel;

    [Header("Objects")]
    [SerializeField] private Text _noItemsText;
    [field: SerializeField] public GameObject UseButton { get; private set; }
    [SerializeField] private Text _nameText;
    [SerializeField] protected Text _descriptionText;
    [SerializeField] private Text _sharpsCountText;

    [field: SerializeField, Header("Values")] protected PlayerInventory PlayerInventory { get; private set; }
    [field: SerializeField] protected BaseInventoryItem VoidItem { get; private set; }
    [SerializeField] private FloatValue _sharps;

    protected BaseInventoryItem _currentItem { get; private set; }

    public void Start()
    {
        SetText(VoidItem);
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        ClearInventory();
        MakeInventorySlots();
        SetText(VoidItem);
        
        if(_sharpsCountText)
            _sharpsCountText.text = _sharps.Value.ToString(); 
    }

    protected void MakeInventorySlots()
    {
        if (_noItemsText != null)
            _noItemsText.gameObject.SetActive(PlayerInventory.MyInventory.Count == 0);

        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            if (PlayerInventory.MyInventory[i])
            {
                if (PlayerInventory.MyInventory[i].ItemData.NumberHeld > 0)
                {
                    GameObject temp = Instantiate(_inventorySlot, _inventoryPanel.transform.position, Quaternion.identity, _inventoryPanel.transform);
                    temp.transform.localScale = Vector3.one;
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                        newSlot.Setup(PlayerInventory.MyInventory[i], this);
                }
            }
        }
    }

    public void SetText(BaseInventoryItem newItem)
    {
        _currentItem = newItem;
        _nameText.text = _currentItem.ItemData.ItemName;
        _descriptionText.text = _currentItem.ItemData.ItemDescription;
        SetButton();
    }

    public virtual void SetButton()
    {
        RectTransform buttonRect = _descriptionText.gameObject.GetComponent<RectTransform>();
        UseButton.SetActive(_currentItem as UsableInventoryItem != null);

        if (_currentItem as UsableInventoryItem != null)
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
        (_currentItem as UsableInventoryItem).Use();

        if (_currentItem.ItemData.NumberHeld <= 0)
        {
            PlayerInventory.Remove(_currentItem);
            SetText(VoidItem);
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
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        
        player.PlayerAnimations.PlayerAnimator.SetFloat("moveX", 0);
        player.PlayerAnimations.PlayerAnimator.SetFloat("moveY", -1);

        SetPlayerAnim(player.PlayerAnimations.HelmetAnimator, player.PlayerAnimations.PlayerAnimator);
        SetPlayerAnim(player.PlayerAnimations.ChestplateAnimator, player.PlayerAnimations.PlayerAnimator);
        SetPlayerAnim(player.PlayerAnimations.LegginsAnimator, player.PlayerAnimations.PlayerAnimator);
    }

    private void SetPlayerAnim(Animator anim, Animator playerAnim)
    {
        if (anim.runtimeAnimatorController)
        {
            anim.SetFloat("moveX", playerAnim.GetFloat("moveX"));
            anim.SetFloat("moveY", playerAnim.GetFloat("moveY"));
        }
    }
}
