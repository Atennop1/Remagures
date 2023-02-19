using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public interface IRandomNPCMover : INPCMover
    {
        Vector2 MoveDirection { get; }
        void ChooseDifferentDirection();
    }
}