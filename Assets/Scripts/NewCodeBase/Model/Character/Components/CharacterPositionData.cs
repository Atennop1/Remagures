using UnityEngine;

namespace Remagures.Model.Character
{
    public readonly struct CharacterPositionData
    {
        public readonly Vector2 PlayerPosition;
        public readonly Vector2 PlayerViewDirection;

        public CharacterPositionData(Vector2 playerPosition, Vector2 playerViewDirection)
        {
            PlayerPosition = playerPosition;
            PlayerViewDirection = playerViewDirection;
        }
    }
}