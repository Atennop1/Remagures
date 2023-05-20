using System;
using Remagures.Model.Character;

namespace Remagures.View
{
    public sealed class AttackView : IAttackView
    {
        private readonly ICharacterAnimations _characterAnimations;
        private const string ATTACK_ANIMATOR_NAME = "attacking";

        public AttackView(ICharacterAnimations characterAnimations)
            => _characterAnimations = characterAnimations ?? throw new ArgumentNullException(nameof(characterAnimations));

        public void PlayAttackAnimation()
            => _characterAnimations.SetBool(ATTACK_ANIMATOR_NAME, true);

        public void StopAttackAnimation()
            => _characterAnimations.SetBool(ATTACK_ANIMATOR_NAME, false);
    }
}