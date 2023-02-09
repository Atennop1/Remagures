using Remagures.Components;
using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public sealed class Interactable : IInteractable
    {
        [field: SerializeField, Header("Interactable Stuff")] public ContextClue ContextClue { get; private set; }
        [field: SerializeField] protected PlayerInteractingHandler PlayerInteract { get; private set; }
        public bool PlayerInRange { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player _) || collision.isTrigger) 
                return;
            
            PlayerInRange = true;
            PlayerInteract.SetCurrentInteractable(this);
            ContextClue.ChangeContext();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player _) || collision.isTrigger) 
                return;
            
            PlayerInRange = false;
            PlayerInteract.ResetCurrentInteractable(this);
            ContextClue.ChangeContext();
        }

        public void Interact()
        {
            
        }
    }
}
