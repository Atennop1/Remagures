using Remagures.Model.CutscenesSystem;
using Remagures.View.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Actions
{
    public class CloseDialogWindowActionFactory : SerializedMonoBehaviour, ICutsceneActionFactory
    {
        [SerializeField] private IDialogView _dialogView;
        private ICutsceneAction _builtCutsceneAction;
        
        public ICutsceneAction Create()
        {
            if (_builtCutsceneAction != null)
                return _builtCutsceneAction;

            _builtCutsceneAction = new CloseDialogWindowAction(_dialogView);
            return _builtCutsceneAction;
        }
    }
}