using UnityEngine.SceneManagement;
using UnityEngine;

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
    public PlayerState CurrentState { get; private set; }
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
    [field: SerializeField] public FloatValue CurrentHealth { get; private set; }

    [field: SerializeField, Header("Objects")] public InventoryUniqueView UniqueManager { get; private set; }
    [field: SerializeField] public MagicCounter MagicCount { get; private set; }
    [SerializeField] private StringValue _currentScene;

    public void Awake()
    {
        PlayerAnimations.SetAnimFloat(new Vector2(0, -1));
        UniqueManager.SetUnique(this);
    }

    public void Start()
    {
        _currentScene.Value = SceneManager.GetActiveScene().name;
        transform.position = _playerPosition.Value;

        CurrentState = PlayerState.Walk;

        ChangeArmor();

        if (UniqueManager.MagicSlot != null && (UniqueManager.MagicSlot.ThisItem as MagicInventoryItem) != null && (UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).MagicItemData.Projectile)
            MagicCount.SetupProjectile((UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).MagicItemData.Projectile.GetComponent<Projectile>());
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

    public void ChangeState(PlayerState newState)
    {
        if (newState == PlayerState.Dead && CurrentHealth.Value > 0)
            throw new System.InvalidOperationException();

        if (newState != CurrentState)
            CurrentState = newState;
    }
}
