using Remagures.Model.CutscenesSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class TeleportActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector2 _position;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new TeleportAction(_transform, _position);
            return _builtCutsceneAction;
        }
    }
}