using Remagures.Model.CutscenesSystem;
using Remagures.Root.Dialogs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public sealed class DialogActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private IDialogPlayerFactory _dialogPlayerFactory;
        [SerializeField] private IDialogFactory _dialogFactory;
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