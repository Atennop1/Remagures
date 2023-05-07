using UnityEngine;

namespace Remagures.Model.Buttons
{
    public sealed class PauseTimeButton : IButton
    {
        public void Press()
            => Time.timeScale = 0;
    }
}