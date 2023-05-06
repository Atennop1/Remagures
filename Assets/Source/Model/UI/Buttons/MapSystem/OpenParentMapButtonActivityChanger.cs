using System;
using Remagures.Model.MapSystem;
using UnityEngine.UI;

namespace Remagures.Model.UI
{
    public sealed class OpenParentMapButtonActivityChanger
    {
        private readonly Button _button;
        private readonly ParentMapOpener _parentMapOpener;

        public OpenParentMapButtonActivityChanger(Button button, ParentMapOpener parentMapOpener)
        {
            _button = button ?? throw new ArgumentNullException(nameof(button));
            _parentMapOpener = parentMapOpener ?? throw new ArgumentNullException(nameof(parentMapOpener));
        }

        public void Change() 
            => _button.enabled = _parentMapOpener.CanOpen();
    }
}