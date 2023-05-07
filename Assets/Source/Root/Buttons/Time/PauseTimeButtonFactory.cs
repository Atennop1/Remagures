using Remagures.Model.UI;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public sealed class PauseTimeButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new PauseTimeButton();
            return _builtButton;
        }
    }
}