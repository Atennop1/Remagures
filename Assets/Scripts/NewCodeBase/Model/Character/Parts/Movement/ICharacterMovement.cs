using UnityEngine;

namespace Remagures.Model.Character
{
    public interface ICharacterMovement
    {
        Transform Transform { get; }
        Vector2 CharacterLookDirection { get; }
        
        bool IsMoving { get; }
        void MoveTo(Vector3 endPosition);
    }
}