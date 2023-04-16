using Remagures.Model.DialogSystem;
using Remagures.View.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class DialogTextWriterFactory : SerializedMonoBehaviour
    {
        [SerializeField] private DialogTextWriterView _dialogTextWriterView;
        private IDialogTextWriter _builtWriter;
        
        public IDialogTextWriter Create()
        {
            if (_builtWriter != null)
                return _builtWriter;

            _builtWriter = new DialogTextWriter(_dialogTextWriterView);
            return _builtWriter;
        }
    }
}