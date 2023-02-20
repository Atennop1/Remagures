using Remagures.View.Character;
using UnityEngine;

namespace Remagures.Model.Character
{
    public class CharacterStatesBuilder //TODO split this to few components and add abstraction
    {
        private readonly CharacterAttacker _characterAttacker;
        private readonly ICharacterInteractor _characterInteractor;
        private readonly CharacterMagicApplier _characterMagicApplier;
        private readonly Rigidbody2D _characterRigidbody;
        private readonly ICharacterMovementView _characterMovementView;
        private readonly ICharacterMovement _characterMovement;

        public CharacterStates Build()
        {
            var standingState = new StandingState(_characterRigidbody, _characterMovementView);
            var walkingState = new WalkingState(_characterMovement);
            var knockbackedState = new KnockbackedState();
            var interactingState = new InteractingState(_characterInteractor);
            var attackingState = new AttackingState(_characterAttacker);
            var magicAttackingState = new MagicAttackingState(_characterMagicApplier);
            var deathState = new DeathState();

            var states = new CharacterStates(standingState, walkingState, knockbackedState, interactingState,
                attackingState, magicAttackingState, deathState);

            return states;
        }
    }
}