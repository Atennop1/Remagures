using UnityEngine;

public class PhysicInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private BaseInventoryItem _thisItem;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            AddItemInInventory();
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.ChangeArmor();
            player.UniqueManager.SetUnique(player);
            player.PlayerMovement.SetDirection();
            player.PlayerAnimations.ChangeAnim("moving", false);
            Destroy(gameObject);
        }
    }

    private void AddItemInInventory()
    {
        if (_playerInventory && _thisItem)
            _playerInventory.Add(_thisItem, true);
    }
}
