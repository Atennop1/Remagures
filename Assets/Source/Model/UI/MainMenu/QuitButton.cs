using UnityEngine;

namespace Remagures.Model
{
    public sealed class QuitButton : IButton
    {
        public void Press()
            => Application.Quit();
    }
}