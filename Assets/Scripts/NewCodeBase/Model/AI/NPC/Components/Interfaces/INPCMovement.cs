using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public interface INPCMovement
    {
        void Move(Vector2 targetPosition);
        void StopMoving();
    }
}