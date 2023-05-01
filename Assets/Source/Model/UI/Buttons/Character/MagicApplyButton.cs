using Remagures.Model.Character;

namespace Remagures.Model.UI
{
    public sealed class MagicApplyButton : IButton
    {
        private readonly CharacterMagicApplier _characterMagicApplier;
        
        public void Press()
        {
            if (_characterMagicApplier.CanApply)
                _characterMagicApplier.Apply();
        }
    }
}