using Remagures.Model.UI;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public sealed class UnpauseTimeButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new UnpauseTimeButton();
            return _builtButton;
        }
    }
}