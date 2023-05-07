using UnityEngine;

namespace Remagures.Model.Buttons
{
    public sealed class QuitButton : IButton
    {
        public void Press()
            => Application.Quit();
    }
}