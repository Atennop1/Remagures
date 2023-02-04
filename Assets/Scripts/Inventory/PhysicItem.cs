using Remagures.Player;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Inventory
{
    public class PhysicItem : MonoBehaviour
    {
        [field: SerializeField] protected PlayerInventory PlayerInventory { get; private set; }
        [field: SerializeField] protected BaseInventoryItem ThisItem { get; private set; }
        
        [SerializeField] private UniqueSetup _uniqueSetup;
        [SerializeField] private UniqueEntryPoint _uniqueEntryPoint;
    
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out Player.Player player) || collision.isTrigger) return;
        
            _uniqueEntryPoint?.UpdateArmor();
            _uniqueSetup?.SetupUnique(player);
            player.GetPlayerComponent<PlayerAnimations>().ChangeAnim("moving", false);

            AddItemInInventory();
        }

        protected virtual bool CanAddItem()
            => PlayerInventory && ThisItem;

        private void AddItemInInventory()
        {
            if (!CanAddItem()) 
                return;
        
            PlayerInventory.Add(new Cell(ThisItem));
            Destroy(gameObject);
        }
    }
}
