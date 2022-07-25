using UnityEngine;

public class PhysicInventoryItem : MonoBehaviour
{
    [field: SerializeField] protected PlayerInventory PlayerInventory { get; private set; }
    [field: SerializeField] protected BaseInventoryItem ThisItem { get; private set; }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player) && !collision.isTrigger)
        {
            player.ChangeArmor();
            player.UniqueSetup.SetUnique(player);
            player.PlayerMovement.SetDirection();
            player.PlayerAnimations.ChangeAnim("moving", false);

            AddItemInInventory();
        }
    }

    protected virtual bool CanAddItem()
    {
        return PlayerInventory && ThisItem;
    }

    private void AddItemInInventory()
    {
        if (CanAddItem())
        {
            PlayerInventory.Add(new Cell(ThisItem, 1));
            Destroy(gameObject);
        }
    }
}
