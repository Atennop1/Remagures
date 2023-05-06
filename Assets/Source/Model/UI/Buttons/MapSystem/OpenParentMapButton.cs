using Remagures.Model.MapSystem;

namespace Remagures.Model.UI
{
    public sealed class OpenParentMapButton : IButton
    {
        private readonly ParentMapOpener _parentMapOpener;

        public void Press()
        {
            if (!_parentMapOpener.CanOpen())
                return;
            
            _parentMapOpener.Open();
        }
    }
}