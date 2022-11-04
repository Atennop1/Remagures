using Remagures.Inventory.View;
using Remagures.Player.Components;
using Remagures.SO.Inventory;
using Remagures.SO.Inventory.Items;
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
        {
            return PlayerInventory && ThisItem;
        }

        private void AddItemInInventory()
        {
            if (!CanAddItem()) return;
        
            PlayerInventory.Add(new Cell(ThisItem));
            Destroy(gameObject);
        }
    }
}
