using Remagures.SO;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public class PhysicItem : MonoBehaviour
    {
        [field: SerializeField] protected Inventory Inventory { get; private set; }
        [field: SerializeField] protected Item ThisItem { get; private set; }
        
        [SerializeField] private UniqueSetup _uniqueSetup;
        [SerializeField] private UniqueEntryPoint _uniqueEntryPoint;
    
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out Player player) || collision.isTrigger) return;
        
            _uniqueEntryPoint?.UpdateArmor();
            _uniqueSetup?.SetupUnique(player);
            player.GetPlayerComponent<PlayerAnimations>().ChangeAnim("moving", false);

            AddItemInInventory();
        }

        protected virtual bool CanAddItem()
            => Inventory && ThisItem;

        private void AddItemInInventory()
        {
            if (!CanAddItem()) 
                return;
        
            Inventory.Add(new Cell(ThisItem));
            Destroy(gameObject);
        }
    }
}
