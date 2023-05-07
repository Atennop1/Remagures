using System;
using Remagures.Model.Character;

namespace Remagures.Model.Buttons
{
    public sealed class MagicApplyButton : IButton
    {
        private readonly CharacterMagicApplier _characterMagicApplier;

        public MagicApplyButton(CharacterMagicApplier characterMagicApplier) 
            => _characterMagicApplier = characterMagicApplier ?? throw new ArgumentNullException(nameof(characterMagicApplier));

        public void Press()
        {
            if (_characterMagicApplier.CanApply)
                _characterMagicApplier.Apply();
        }
    }
}