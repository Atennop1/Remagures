using UnityEngine;

namespace Remagures.View.Interactable
{
    public interface INPCMovementView
    {
        void DisplayMovement();
        void DisplayStaying();
        void DisplayMovementDirection(Vector2 direction);
    }
}