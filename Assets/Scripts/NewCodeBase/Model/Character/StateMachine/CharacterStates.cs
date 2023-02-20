using System;
using Remagures.Model.AI;

namespace Remagures.Model.Character
{
    public struct CharacterStates
    {
        public readonly IState StandingState;
        public readonly IState WalkingState;
        public readonly IState KnockbackedState;
        public readonly IState InteractingState;
        public readonly IState AttackingState;
        public readonly IState MagicAttackingState;
        public readonly IState DeathState;

        public CharacterStates(IState standingState, IState walkingState, IState knockbackedState, IState interactingState, 
            IState attackingState, IState magicAttackingState, IState deathState)
        {
            StandingState = standingState ?? throw new ArgumentNullException(nameof(standingState));
            WalkingState = walkingState ?? throw new ArgumentNullException(nameof(walkingState));
            KnockbackedState = knockbackedState ?? throw new ArgumentNullException(nameof(knockbackedState));
            InteractingState = interactingState ?? throw new ArgumentNullException(nameof(interactingState));
            AttackingState = attackingState ?? throw new ArgumentNullException(nameof(attackingState));
            MagicAttackingState = magicAttackingState ?? throw new ArgumentNullException(nameof(magicAttackingState));
            DeathState = deathState ?? throw new ArgumentNullException(nameof(deathState));
        }
    }
}