using Remagures.Model.Buttons;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public sealed class RestartButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new RestartButton();
            return _builtButton;
        }
    }
}