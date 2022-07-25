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

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(PlayerInteracting))]
public class Player : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public int TotalArmor { get; private set; }

    public PlayerAnimations PlayerAnimations { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerInteracting PlayerInteracting { get; private set; }

    [Header("ScriptableObjects")]
    [SerializeField] private VectorValue _playerPosition;
    [field: SerializeField] public ClassStat ClassStat { get; private set; }
    [field: SerializeField] public FloatValue CurrentHealth { get; private set; }

    [field: SerializeField, Header("Objects")] public InventoryUniqueSetup UniqueSetup { get; private set; }
    [field: SerializeField] public MagicCounter MagicCounter { get; private set; }
    [SerializeField] private StringValue _currentScene;

    private void Awake()
    {
        PlayerAnimations = GetComponent<PlayerAnimations>();
        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerInteracting = GetComponent<PlayerInteracting>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    public void Start()
    {
        UniqueSetup.SetUnique(this);
        _currentScene.Value = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt("Visited" + SceneManager.GetActiveScene().path, 1);

        transform.position = _playerPosition.Value - new Vector2(0, 0.6f);
        PlayerAnimations.SetAnimFloat(new Vector2(0, -1));
        PlayerAnimations.ChangeAnim("moving", false);
        CurrentState = PlayerState.Walk;

        ChangeArmor();
        if (UniqueSetup.MagicSlot != null && (UniqueSetup.MagicSlot.ThisCell.Item as IMagicItem) != null && (UniqueSetup.MagicSlot.ThisCell.Item as IMagicItem).Projectile)
            MagicCounter.SetupProjectile((UniqueSetup.MagicSlot.ThisCell.Item as IMagicItem).Projectile.GetComponent<Projectile>());
    }

    public void ChangeArmor()
    {
        float temp = 0;
        
        foreach (IReadOnlyCell cell in UniqueSetup.PlayerInventory.MyInventory)
        {
            IArmorItem armorItem = cell.Item as IArmorItem;
            if (armorItem != null)
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
