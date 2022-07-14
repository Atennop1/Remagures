using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private FloatValue _sharps;
    [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    [SerializeField] private Player _player;

    [field: SerializeField, Header("Objects")] public Text SharpsCountText { get; private set; }
    [SerializeField] private GameObject _noItems;
    [SerializeField] private GameObject _slotPrefab;

    public void OnEnable()
    {
        ClearInventory();
        CreateSlots();
        SharpsCountText.text = _sharps.Value.ToString(); 
    }

    public void CreateSlots()
    {
        _noItems.SetActive(true);
        foreach (IReadOnlyCell cell in Inventory.MyInventory)
        {
            IUpgradableItem currentItem = cell.Item as IUpgradableItem;

            if (currentItem != null && 
            currentItem.ItemsForLevels != null && 
            currentItem.ThisItemLevel != 0 && 
            currentItem.ThisItemLevel < currentItem.ItemsForLevels.Count && 
            _sharps.Value >= (currentItem.ItemsForLevels[currentItem.ThisItemLevel] as IUpgradableItem).CostForThisItem)
            {
                GameObject temp = Instantiate(_slotPrefab, transform.position, Quaternion.identity, transform);
                temp.GetComponent<UpgradeSlot>().Setup(new Cell(currentItem.ItemsForLevels[currentItem.ThisItemLevel], 1), this, _player);
                _noItems.SetActive(false);
            }
        }
    }

    private void ClearInventory()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    public void Close()
    {
        Time.timeScale = 1;
        gameObject.transform.parent.parent.parent.parent.gameObject.SetActive(false);
        _player.Awake();
    }
}
