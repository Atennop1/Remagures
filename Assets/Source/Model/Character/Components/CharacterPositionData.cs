using UnityEngine;

namespace Remagures.Model.Character
{
    public readonly struct CharacterPositionData
    {
        public readonly Vector2 CharacterPosition;
        public readonly Vector2 CharacterLookDirection;

        public CharacterPositionData(Vector2 characterPosition, Vector2 characterLookDirection)
        {
            CharacterPosition = characterPosition;
            CharacterLookDirection = characterLookDirection;
        }
    }
}