using UnityEngine;

namespace Remagures.Model.Character
{
    public interface ICharacterAnimations
    {
        void SetCharacterLookDirection(Vector2 direction);
        void SetBool(string key, bool value);
        void SetAnim(string key); //TODO make contract using method to every animation
    }
}