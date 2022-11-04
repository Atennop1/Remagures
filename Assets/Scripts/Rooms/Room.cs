using Remagures.AI.Enemies.Abstraction;
using Remagures.AI.Enemies.Components;
using Remagures.Interactable;
using UnityEngine;

namespace Remagures.Rooms
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] protected Enemy[] Enemies { get; private set; }
        [field: SerializeField] protected Destroyable[] Pots { get; private set; }
    
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player.Player _) || other.isTrigger) return;
            ChangeActivationOfAll(true);
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player.Player _) || other.isTrigger) return;
            ChangeActivationOfAll(false);
        }

        private void ChangeActivationOfAll(bool activation)
        {
            foreach (var enemy in Enemies)
                ChangeActivation(enemy, activation);

            foreach (var pot in Pots)
                ChangeActivation(pot, activation);
        }
    
        protected void ChangeActivation(Component component, bool activation)
        {
            component.gameObject.SetActive(activation);
        }
    }
}
