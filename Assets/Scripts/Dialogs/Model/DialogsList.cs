using System;
using System.Linq;
using Remagures.SaveSystem;

namespace Remagures.Dialogs.Model
{
    public class DialogsList<T> : IDialogsList<T>
    {
        public Dialog CurrentDialog { get; private set; }
        private readonly Dialog[] _dialogs;
        private readonly StorageWithNames<T, Dialog> _storage;

        public DialogsList(Dialog[] dialogs)
        {
            _storage = new StorageWithNames<T, Dialog>();
            _dialogs = dialogs ?? throw new ArgumentException("DialogList can't be null");
            
            if (_storage.Exist())
                CurrentDialog = _storage.Load<Dialog>();
        }

        public void SwitchToDialog(string dialogName)
        {
            if (_dialogs.ToList().Find(dialog => dialog.Name == dialogName) == null)
                throw new ArgumentException($"DialogsList doesn't contains dialog with name {dialogName}");
            
            CurrentDialog = _dialogs.ToList().Find(dialog => dialog.Name == dialogName);
        }
        
        public void SwitchToDialog(Dialog dialog)
        {
            CurrentDialog = dialog ?? throw new ArgumentException("Dialog can't be null");
        }

        public void SaveCurrentDialog()
        {
            _storage.Save(CurrentDialog);
        }
    }
}