using Remagures.Model.CutscenesSystem;
using Remagures.Root.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class DialogActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private DialogPlayerFactory _dialogPlayerFactory;
        [SerializeField] private DialogFactory _dialogFactory;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new DialogAction(_dialogPlayerFactory.Create(), _dialogFactory.Create());
            return _builtCutsceneAction;
        }
    }
}