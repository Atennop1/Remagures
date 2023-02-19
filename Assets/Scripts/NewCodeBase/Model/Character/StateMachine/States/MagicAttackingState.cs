using System;
using Remagures.Model.AI.StateMachine;

namespace Remagures.Model.Character
{
    public class MagicAttackingState : IState
    {
        private readonly CharacterMagicApplier _characterMagicApplier;

        public MagicAttackingState(CharacterMagicApplier characterMagicApplier)
            => _characterMagicApplier = characterMagicApplier ?? throw new ArgumentNullException(nameof(characterMagicApplier));

        public void OnEnter()
            => _characterMagicApplier.Apply();

        public void Update() { }
        public void OnExit() { }
    }
}