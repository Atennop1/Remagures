using Remagures.Model.Character;

namespace Remagures.Model
{
    public sealed class AttackButton : IButton
    {
        private readonly CharacterAttacker _characterAttacker;
        private readonly ICharacterInteractor _characterInteractor;
        
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