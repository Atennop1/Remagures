using Remagures.Model.CutscenesSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class WaitActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private float _delay;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new WaitAction(_delay);
            return _builtCutsceneAction;
        }
    }
}