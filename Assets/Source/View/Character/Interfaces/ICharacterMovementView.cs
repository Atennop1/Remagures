using UnityEngine;

namespace Remagures.View.Character
{
    public interface ICharacterMovementView
    {
        void StartMoveAnimation();
        void EndMoveAnimation();
        void DisplayCharacterLookDirection(Vector2 direction);
    }
}