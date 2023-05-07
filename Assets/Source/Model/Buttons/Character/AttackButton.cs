using System;
using Remagures.Model.Character;

namespace Remagures.Model.Buttons
{
    public sealed class AttackButton : IButton //TODO make composite for it and magic button
    {
        private readonly CharacterAttacker _characterAttacker;
        private readonly ICharacterInteractor _characterInteractor;

        public AttackButton(CharacterAttacker characterAttacker, ICharacterInteractor characterInteractor)
        {
            _characterAttacker = characterAttacker ?? throw new ArgumentNullException(nameof(characterAttacker));
            _characterInteractor = characterInteractor ?? throw new ArgumentNullException(nameof(characterInteractor));
        }

        public void Press()
        {
            if (_characterInteractor.CurrentState == InteractionState.Ready)
            {
                _characterInteractor.Interact();
                return;
            }
            
            if (_characterAttacker.CanAttack)
                _characterAttacker.Attack();
        }
    }
}