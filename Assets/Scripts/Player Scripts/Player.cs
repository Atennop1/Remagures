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

public class Player : MonoBehaviour
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

    [field: SerializeField, Header("Objects")] public InventoryUniqueSetup UniqueSetup { get; private set; }
    [field: SerializeField] public MagicCounter MagicCount { get; private set; }
    [SerializeField] private StringValue _currentScene;

    public void Awake()
    {
        UniqueSetup.SetUnique(this);
        PlayerAnimations.SetAnimFloat(new Vector2(0, -1));
        PlayerAnimations.ChangeAnim("moving", false);
    }

    public void Start()
    {
        UniqueSetup.SetUnique(this);
        _currentScene.Value = SceneManager.GetActiveScene().name;
        transform.position = _playerPosition.Value;

        CurrentState = PlayerState.Walk;

        ChangeArmor();

        if (UniqueSetup.MagicSlot != null && (UniqueSetup.MagicSlot.ThisCell.Item as IMagicItem) != null && (UniqueSetup.MagicSlot.ThisCell.Item as IMagicItem).Projectile)
            MagicCount.SetupProjectile((UniqueSetup.MagicSlot.ThisCell.Item as IMagicItem).Projectile.GetComponent<Projectile>());
    }

    public void ChangeArmor()
    {
        float temp = 0;
        
        foreach (IReadOnlyCell cell in UniqueSetup.PlayerInventory.MyInventory)
        {
            IArmorItem armorItem = cell.Item as IArmorItem;
            if ( armorItem != null)
                temp += armorItem.Armor;
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
