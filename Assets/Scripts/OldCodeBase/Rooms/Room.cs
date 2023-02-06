using Remagures.AI.Enemies;
using Remagures.Interactable;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Rooms
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] protected Enemy[] Enemies { get; private set; }
        [field: SerializeField] protected Destroyable[] Pots { get; private set; }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) || other.isTrigger) return;
            ChangeActivationOfAll(true);
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) || other.isTrigger) return;
            ChangeActivationOfAll(false);
        }

        protected void ChangeActivationOfAll(bool activation)
        {
            foreach (var enemy in Enemies)
                ChangeActivation(enemy, activation);

            foreach (var pot in Pots)
                ChangeActivation(pot, activation);
        }

        private void ChangeActivation(Component component, bool activation)
            => component.gameObject.SetActive(activation);
    }
}
