using Remagures.Player.Components;
using Remagures.SO.Other;
using UnityEngine;

namespace Remagures.Interactable.Abstraction
{
    public abstract class Interactable : MonoBehaviour
    {
        [field: SerializeField, Header("Interactable Stuff")] public Signal Context { get; private set; }
        [field: SerializeField] protected PlayerInteractingHandler PlayerInteract { get; private set; }
        public bool PlayerInRange { get; private set; }

        public void DetectInteracting()
        {
            if (!PlayerInRange) return;
        
            PlayerInteract.SetCurrentInteractable(this);
            Context.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _) && !collision.isTrigger)
            {
                PlayerInRange = true;
                DetectInteracting();
            }

            if (CanTriggerEnter(collision))
                TriggerEnter();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _) && !collision.isTrigger)
            {
                PlayerInRange = false;
                PlayerInteract.ResetCurrentInteractable(this);
                Context.Invoke();
            }

            if (CanTriggerExit(collision))
                TriggerExit();
        }

        protected virtual void TriggerEnter() { }
        protected virtual void TriggerExit() { }

        protected virtual bool CanTriggerEnter(Collider2D collision) { return true; }
        protected virtual bool CanTriggerExit(Collider2D collision) { return true; }

        public abstract void Interact();
    }
}
