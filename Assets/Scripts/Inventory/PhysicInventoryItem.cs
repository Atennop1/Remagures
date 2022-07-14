using UnityEngine;

public class PhysicInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private BaseInventoryItem _thisItem;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player) && !collision.isTrigger)
        {
            AddItemInInventory();

            player.ChangeArmor();
            player.UniqueSetup.SetUnique(player);
            player.PlayerMovement.SetDirection();
            player.PlayerAnimations.ChangeAnim("moving", false);
            
            Destroy(gameObject);
        }
    }

    private void AddItemInInventory()
    {
        if (_playerInventory && _thisItem)
            _playerInventory.Add(new Cell(_thisItem, 1));
    }
}
