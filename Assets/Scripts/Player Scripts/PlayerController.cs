using UnityEngine.SceneManagement;
using UnityEngine;

[HideInInspector]
public enum PlayerState
{
    Idle,
    Walk,
    Stagger,
    Attack,
    Interact,
    Dead
}

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerState CurrentState;
    public int TotalArmor { get; private set; }

    [field: SerializeField, Header("Usable Components")] public PlayerAnimations PlayerAnimations { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerAttack PlayerAttack { get; private set; }
    [field: SerializeField] public PlayerInteracting PlayerInteract { get; private set; }

    [Header("ScriptableObjects")]
    [SerializeField] private VectorValue _playerPosition;
    [field: SerializeField] public ItemValue CurrentItem { get; private set; }
    [field: SerializeField] public ClassStat ClassStat { get; private set; }
    [field: SerializeField] public Signal DecreaseMagicSignal { get; private set; }

    [field: SerializeField, Header("Objects")] public InventoryManagerUnique UniqueManager { get; private set; }
    [field: SerializeField] public MagicManager MagicManager { get; private set; }
    [SerializeField] private StringValue _currentScene;

    private void Start()
    {
        _currentScene.Value = SceneManager.GetActiveScene().name;
        transform.position = _playerPosition.Value;

        CurrentState = PlayerState.Walk;
        PlayerAnimations.ChangeAnim("moveX", 0);
        PlayerAnimations.ChangeAnim("moveY", -1);

        UniqueManager.SetUnique(this);
        ChangeArmor();

        if (UniqueManager.MagicSlot != null && (UniqueManager.MagicSlot.ThisItem as MagicInventoryItem) != null && (UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).MagicItemData.Projectile)
            MagicManager.SetupProjectile((UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).MagicItemData.Projectile.GetComponent<Projectile>());
    }

    public void ChangeArmor()
    {
        float temp = 0;
        
        foreach (BaseInventoryItem item in UniqueManager.PlayerInventory.MyInventory)
        {
            ArmorInventoryItem armorItem = item as ArmorInventoryItem;
            if (armorItem != null && (armorItem.UniqueItemData.UniqueClass == UniqueClass.Helmet || armorItem.UniqueItemData.UniqueClass == UniqueClass.Chestplate || armorItem.UniqueItemData.UniqueClass == UniqueClass.Leggins))
                temp += armorItem.ArmorItemData.Armor;
        }

        TotalArmor = Mathf.RoundToInt(temp);
    }
}
