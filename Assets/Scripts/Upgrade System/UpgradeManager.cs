using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private FloatValue _sharps;
    [field: SerializeField] public PlayerInventory Inventory { get; private set; }

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
        foreach (BaseInventoryItem item in Inventory.MyInventory)
        {
            UpgradableInventoryItem currentItem = item as UpgradableInventoryItem;
            if (currentItem != null && 
            currentItem.UpgradableItemData.ItemsForLevels != null && 
            currentItem.UpgradableItemData.ThisItemLevel != 0 && 
            currentItem.UpgradableItemData.ThisItemLevel < currentItem.UpgradableItemData.ItemsForLevels.Count && 
            _sharps.Value >= (currentItem.UpgradableItemData.ItemsForLevels[currentItem.UpgradableItemData.ThisItemLevel] as UpgradableInventoryItem).UpgradableItemData.CostForThisItem)
            {
                GameObject temp = Instantiate(_slotPrefab, transform.position, Quaternion.identity, transform);
                temp.GetComponent<UpgradeSlot>().Setup(currentItem.UpgradableItemData.ItemsForLevels[currentItem.UpgradableItemData.ThisItemLevel], this);
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
        gameObject.transform.parent.parent.parent.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
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
