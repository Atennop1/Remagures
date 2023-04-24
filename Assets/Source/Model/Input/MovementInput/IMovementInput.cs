using UnityEngine;

namespace Remagures.Model.Input
{
    public interface IMovementInput
    {
        Vector2 CharacterLookDirection { get; }
        Vector2 MoveDirection { get; }
    }
}