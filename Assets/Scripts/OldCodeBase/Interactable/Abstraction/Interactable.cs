using Remagures.Components;
using Remagures.Model.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Interactable
{
    public abstract class Interactable : SerializedMonoBehaviour
    {
        [field: SerializeField, Header("Interactable Stuff")] public ContextClue ContextClue { get; private set; }
        [field: SerializeField] protected PlayerInteractingHandler PlayerInteract { get; private set; }
        public bool PlayerInRange { get; private set; }

        public void DetectInteracting()
        {
            if (!PlayerInRange) return;
        
            PlayerInteract.SetCurrentInteractable(this);
            ContextClue.ChangeContext();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player _) && !collision.isTrigger)
            {
                PlayerInRange = true;
                DetectInteracting();
            }

            if (CanTriggerEnter(collision))
                TriggerEnter();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player _) && !collision.isTrigger)
            {
                PlayerInRange = false;
                PlayerInteract.ResetCurrentInteractable(this);
                ContextClue.ChangeContext();
            }

            if (CanTriggerExit(collision))
                TriggerExit();
        }

        protected virtual void TriggerEnter() { }
        protected virtual void TriggerExit() { }

        protected virtual bool CanTriggerEnter(Collider2D collision) 
            => true; 
        
        protected virtual bool CanTriggerExit(Collider2D collision)
            => true; 

        public abstract void Interact();
    }
}
