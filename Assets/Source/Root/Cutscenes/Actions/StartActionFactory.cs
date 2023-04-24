using Remagures.Model.CutscenesSystem;
using Remagures.View;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class StartActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private UIActivityChanger _uiActivityChanger;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new StartAction(_uiActivityChanger);
            return _builtCutsceneAction;
        }
    }
}