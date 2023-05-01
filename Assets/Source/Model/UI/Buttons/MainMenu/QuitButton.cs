using UnityEngine;

namespace Remagures.Model.UI
{
    public sealed class QuitButton : IButton
    {
        public void Press()
            => Application.Quit();
    }
}