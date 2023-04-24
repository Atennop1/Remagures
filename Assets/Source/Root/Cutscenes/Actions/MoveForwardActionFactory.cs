using Remagures.Model.Character;
using Remagures.Model.CutscenesSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class MoveForwardActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private CharacterMovement _characterMovement;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new MoveAction(_characterMovement, (Vector2)_characterMovement.Transform.position + _characterMovement.CharacterLookDirection * 2);
            return _builtCutsceneAction;
        }
    }
}