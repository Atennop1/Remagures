using System;
using Remagures.Model.AI;

namespace Remagures.Model.Character
{
    public sealed class AttackingState : IState
    {
        private readonly CharacterAttacker _characterAttacker;

        public AttackingState(CharacterAttacker characterAttacker)
            => _characterAttacker = characterAttacker ?? throw new ArgumentNullException(nameof(characterAttacker));

        public void OnEnter()
            => _characterAttacker.UseAttack();

        public void Update() { }
        public void OnExit() { }
    }
}