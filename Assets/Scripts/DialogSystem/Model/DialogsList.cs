using System;
using System.Linq;
using Remagures.SaveSystem.SwampAttack.Runtime.Tools.SaveSystem;

namespace Remagures.DialogSystem.Model
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

            if (_storage.Exist($"Dialog-{_characterName}"))
                CurrentDialog = _storage.Load<Dialog>($"Dialog-{_characterName}");
        }

        public void SwitchToDialog(string dialogName)
        {
            if (_dialogs.ToList().Find(dialog => dialog.Name == dialogName) == null)
                throw new ArgumentException($"DialogsList doesn't contains dialog with name {dialogName}");

            CurrentDialog = _dialogs.ToList().Find(dialog => dialog.Name == dialogName);
        }

        public void SwitchToDialog(Dialog dialog)
            => CurrentDialog = dialog ?? throw new ArgumentException("Dialog can't be null");

        public void SaveCurrentDialog()
            => _storage.Save(CurrentDialog, $"Dialog-{_characterName}");
    }
}