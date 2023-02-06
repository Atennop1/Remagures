using System.Linq;
using Remagures.Interactable;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Rooms
{
    public class DungeonEnemyRoom : DungeonRoom
    {
        [SerializeField] private Door[] _doors;
    
        public void CheckEnemies()
        {
            if (Enemies.Any(enemy => enemy.gameObject.activeInHierarchy))
                return;

            OpenDoors();
        }

        private void CloseDoors()
        {
            foreach(var door in _doors)
                door.CloseDoor();
        }

        private void OpenDoors()
        {
            foreach (var door in _doors)
                door.Interact();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) || other.isTrigger) 
                return;
            
            ChangeActivationOfAll(true);
            CloseDoors();
        }
    }
}
