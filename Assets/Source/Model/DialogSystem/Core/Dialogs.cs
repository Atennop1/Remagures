using System;
using System.Linq;
using SaveSystem;
using SaveSystem.Paths;

namespace Remagures.Model.DialogSystem
{
    public sealed class Dialogs : IDialogs
    {
        public IDialog CurrentDialog { get; private set; }
        private readonly IDialog[] _dialogs;

        private readonly BinaryStorage<IDialog> _storage;

        public Dialogs(IDialog[] dialogs, string characterName)
        {
            if (characterName == null)
                throw new ArgumentNullException(nameof(characterName));
            
            _dialogs = dialogs ?? throw new ArgumentException("DialogList can't be null");
            _storage = new BinaryStorage<IDialog>(new Path($"Dialog-{characterName}"));

            if (!_storage.HasSave()) 
                return;
            
            var loadedDialog = _storage.Load();
            CurrentDialog = dialogs.ToList().Find(dialog => dialog == loadedDialog);
        }

        public void SwitchCurrent(string dialogName)
        {
            var dialogToSwitch = _dialogs.ToList().Find(dialog => dialog.Name == dialogName);

            CurrentDialog = dialogToSwitch ?? throw new ArgumentException($"DialogsList doesn't contains dialog with name {dialogName}");
            _storage.Save(CurrentDialog);
        }
    }
}