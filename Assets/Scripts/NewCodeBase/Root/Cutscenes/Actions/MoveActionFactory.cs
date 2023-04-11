using Remagures.Model.Character;
using Remagures.Model.CutscenesSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class MoveActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private Vector2 _position;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new MoveAction(_characterMovement, _position);
            return _builtCutsceneAction;
        }
    }
}