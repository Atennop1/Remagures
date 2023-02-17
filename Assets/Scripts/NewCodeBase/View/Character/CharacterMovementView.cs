using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.View.Character
{
    public class CharacterMovementView : MonoBehaviour
    {
        private const string MOVE_ANIMATOR_NAME = "moving";
        private ICharacterAnimations _characterAnimations;

        public void StartMoveAnimation()
            => _characterAnimations.SetBool(MOVE_ANIMATOR_NAME, true);

        public void EndMoveAnimation()
            => _characterAnimations.SetBool(MOVE_ANIMATOR_NAME, false);

        public void DisplayCharacterLookDirection(Vector2 direction)
            => _characterAnimations.SetCharacterLookDirection(direction);
    }
}