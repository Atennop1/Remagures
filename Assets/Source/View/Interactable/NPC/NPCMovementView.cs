using Remagures.Model.AI.NPC;
using UnityEngine;

namespace Remagures.View.Interactable
{
    public sealed class NPCMovementView : INPCMovementView
    {
        private readonly NPCAnimations _npcAnimations;
        
        public void DisplayMovementDirection(Vector2 direction)
            => _npcAnimations.SetAnimationsVector(direction);

        public void DisplayMovement()
            => _npcAnimations.DeactivateIsStaying();

        public void DisplayStaying()
            => _npcAnimations.ActivateIsStaying();
    }
}