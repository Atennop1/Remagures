using System;
using System.Linq;
using Remagures.Tools;
using SaveSystem;
using SaveSystem.Paths;

namespace Remagures.Model.DialogSystem
{
    public sealed class Dialogs : IDialogs
    {
        public Dialog CurrentDialog { get; private set; }
        private readonly Dialog[] _dialogs;

        private readonly BinaryStorage<Dialog> _storage;

        public Dialogs(Dialog[] dialogs, string characterName)
        {
            if (characterName == null)
                throw new ArgumentNullException(nameof(characterName));
            
            _dialogs = dialogs ?? throw new ArgumentException("DialogList can't be null");
            _storage = new BinaryStorage<Dialog>(new Path($"Dialog-{characterName}"));

            if (!_storage.HasSave()) 
                return;
            
            var loadedDialog = _storage.Load();
            CurrentDialog = dialogs.ToList().Find(dialog => dialog == loadedDialog);
        }

        public void SwitchCurrent(string dialogName)
        {
            var dialogToSwitch = _dialogs.ToList().Find(dialog => dialog.Name == dialogName);
            
            if (dialogToSwitch == null)
                throw new ArgumentException($"DialogsList doesn't contains dialog with name {dialogName}");

            CurrentDialog = dialogToSwitch;
            _storage.Save(CurrentDialog);
        }
    }
}