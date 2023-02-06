using System;
using System.Linq;
using Remagures.Tools.SwampAttack.Runtime.Tools.SaveSystem;

namespace Remagures.Model.DialogSystem
{
    public class DialogsList : IDialogsList
    {
        public Dialog CurrentDialog { get; private set; }
        private readonly Dialog[] _dialogs;

        private readonly BinaryStorage _storage;
        private readonly string _characterName;

        public DialogsList(Dialog[] dialogs, string characterName)
        {
            _storage = new BinaryStorage();
            _characterName = characterName ?? throw new ArgumentException("CharacterName can't be null");
            _dialogs = dialogs ?? throw new ArgumentException("DialogList can't be null");

            if (!_storage.Exist($"Dialog-{_characterName}")) 
                return;
            
            var loadedDialog = _storage.Load<Dialog>($"Dialog-{_characterName}");
            CurrentDialog = dialogs.ToList().Find(dialog => dialog == loadedDialog);
        }

        public void SwitchToDialog(string dialogName)
        {
            var dialogToSwitch = _dialogs.ToList().Find(dialog => dialog.Name == dialogName);
            
            if (dialogToSwitch == null)
                throw new ArgumentException($"DialogsList doesn't contains dialog with name {dialogName}");

            CurrentDialog = dialogToSwitch;
        }

        public void SwitchToDialog(Dialog dialog)
            => CurrentDialog = dialog ?? throw new ArgumentException("Dialog can't be null");

        public void SaveCurrentDialog()
            => _storage.Save(CurrentDialog, $"Dialog-{_characterName}");
    }
}