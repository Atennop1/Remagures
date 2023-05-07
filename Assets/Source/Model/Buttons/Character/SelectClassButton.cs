using System;
using Remagures.Model.Character;
using SaveSystem;

namespace Remagures.Model.Buttons
{
    public sealed class SelectClassButton : IButton
    {
        private readonly ISaveStorage<CharacterClass> _classStorage;
        private readonly CharacterClass _selectingClass;

        public SelectClassButton(ISaveStorage<CharacterClass> classStorage, CharacterClass selectingClass)
        {
            _classStorage = classStorage ?? throw new ArgumentNullException(nameof(classStorage));
            _selectingClass = selectingClass;
        }

        public void Press() 
            => _classStorage.Save(_selectingClass);
    }
}